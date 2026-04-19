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

        private string _currentSort = "Date (Newest)";

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
        public partial Category? NewTransactionCategory { get; set; }

        public List<Category> Categories { get; } = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

        [RelayCommand]
        private void ToggleAddForm() => IsAddFormVisible = !IsAddFormVisible;

        [RelayCommand]
        private async Task ShowSortOptionsAsync()
        {
            string action = await Shell.Current.DisplayActionSheet("Sort Transactions", "Cancel", null,
                "Date (Newest)", "Date (Oldest)", "Amount (Highest)", "Amount (Lowest)");

            if (!string.IsNullOrEmpty(action) && action != "Cancel")
            {
                _currentSort = action;
                ApplySort();
            }
        }

        private void ApplySort(IEnumerable<TransactionDBModel> sourceList = null)
        {
            var listToSort = sourceList?.ToList() ?? Transactions.ToList();
            if (!listToSort.Any()) return;

            List<TransactionDBModel> sortedList;

            switch (_currentSort)
            {
                case "Date (Oldest)":
                    sortedList = listToSort.OrderBy(t => t.DateTimeOfTransaction).ToList();
                    break;
                case "Amount (Highest)":
                    sortedList = listToSort.OrderByDescending(t => Math.Abs(t.Amount)).ToList();
                    break;
                case "Amount (Lowest)":
                    sortedList = listToSort.OrderBy(t => Math.Abs(t.Amount)).ToList();
                    break;
                case "Date (Newest)":
                default:
                    sortedList = listToSort.OrderByDescending(t => t.DateTimeOfTransaction).ToList();
                    break;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Transactions.Clear();
                foreach (var t in sortedList)
                {
                    Transactions.Add(t);
                }
            });
        }

        [RelayCommand]
        private async Task AddTransactionAsync()
        {
            if (NewTransactionAmount == 0)
            {
                await Shell.Current.DisplayAlertAsync("eror", "enter sum!! >:( expenses should be with '-'", "ok");
                return;
            }

            if (NewTransactionCategory == null)
            {
                await Shell.Current.DisplayAlertAsync("error!!!", "choose category!!!!!", "ladno...");
                return;
            }

            IsBusy = true;

            try
            {
                var amountToSave = NewTransactionAmount;
                var categoryToSave = NewTransactionCategory.Value;
                var descToSave = NewTransactionDescription;
                var dateToSave = DateTime.Now;

                await _transactionService.AddTransactionAsync(_currentWalletId, amountToSave, categoryToSave, descToSave);

                WalletBalance += amountToSave;

                var newTx = new TransactionDBModel
                {
                    Id = Guid.NewGuid(),
                    WalletId = _currentWalletId,
                    Amount = amountToSave,
                    Category = categoryToSave,
                    Description = descToSave,
                    DateTimeOfTransaction = dateToSave
                };

                var currentList = Transactions.ToList();
                currentList.Add(newTx);

                ApplySort(currentList);

                NewTransactionAmount = 0;
                NewTransactionDescription = string.Empty;
                NewTransactionCategory = null;
                IsAddFormVisible = false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("error", $"can't add: {ex.Message}", "OK");
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

                ApplySort(transactions);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("error", ex.Message, "okk");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteTransactionAsync(TransactionDBModel transaction)
        {
            if (transaction == null) return;
            bool confirm = await Shell.Current.DisplayAlertAsync("confirmation", "are you sure you want delete this transactin?", "yes", "no");
            if (!confirm) return;

            IsBusy = true;

            try
            {
                await _transactionService.DeleteTransactionAsync(transaction.Id, _currentWalletId);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    WalletBalance -= transaction.Amount;
                    Transactions.Remove(transaction);
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("error", $"can't delete: {ex.Message}", "ladno");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
