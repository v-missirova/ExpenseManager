using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using ExpenseManager.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    [QueryProperty(nameof(WalletIdString), "WalletId")]
    public partial class WalletDetailsViewModel : BaseViewModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IWalletService _walletService;

        private Guid _currentWalletId;

        public ObservableCollection<TransactionDBModel> Transactions { get; } = new();

        [ObservableProperty]
        public partial string WalletName { get; set; }

        [ObservableProperty]
        public partial decimal WalletBalance { get; set; }

        [ObservableProperty]
        public partial string WalletCurrency { get; set; }
        [ObservableProperty]
        public partial TransactionDBModel? SelectedTransaction { get; set; }
        [ObservableProperty]
        public partial bool IsAddFormVisible { get; set; }

        [ObservableProperty]
        public partial decimal NewTransactionAmount { get; set; }

        [ObservableProperty]
        public partial string NewTransactionDescription { get; set; }

        [ObservableProperty]
        public partial Category NewTransactionCategory { get; set; }

        public List<Category> Categories { get; } = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

        [RelayCommand]
        private void ToggleAddForm() => IsAddFormVisible = !IsAddFormVisible;

        [RelayCommand]
        private async Task AddTransactionAsync()
        {
            if (NewTransactionAmount == 0)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Enter sum (expenses should be negative)", "ok..");
                return;
            }

            IsBusy = true;

            try
            {
                var amountToSave = NewTransactionAmount;
                var categoryToSave = NewTransactionCategory;
                var descToSave = NewTransactionDescription;
                var dateToSave = DateTime.Now;

                await _transactionService.AddTransactionAsync(_currentWalletId, amountToSave, 
                    categoryToSave, descToSave);

                WalletBalance += amountToSave;

                Transactions.Insert(0, new TransactionDBModel
                {
                    Id = Guid.NewGuid(),
                    WalletId = _currentWalletId,
                    Amount = amountToSave,
                    Category = categoryToSave,
                    Description = descToSave,
                    DateTimeOfTransaction = dateToSave
                });

                NewTransactionAmount = 0;
                NewTransactionDescription = string.Empty;
                IsAddFormVisible = false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Помилка бази даних", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        partial void OnSelectedTransactionChanged(TransactionDBModel? value)
        {
            if (value != null)
            {
                Shell.Current.GoToAsync($"TransactionDetailsPage?TransactionId={value.Id}");

                SelectedTransaction = null;
            }
        }

        public string WalletIdString
        {
            set
            {
                if (Guid.TryParse(value, out Guid parsedId))
                {
                    _currentWalletId = parsedId;
                    _ = LoadDataAsync();
                }
            }
        }

        public WalletDetailsViewModel(ITransactionService transactionService, IWalletService walletService)
        {
            _transactionService = transactionService;
            _walletService = walletService;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var wallet = await _walletService.GetWalletByIdAsync(_currentWalletId);
                if (wallet != null)
                {
                    WalletName = wallet.Name;
                    WalletBalance = wallet.Balance;
                    WalletCurrency = wallet.Currency;
                }

                var transactions = await _transactionService.GetTransactionsByWalletIdAsync(_currentWalletId);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Transactions.Clear();
                    foreach (var t in transactions)
                    {
                        Transactions.Add(t);
                    }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}