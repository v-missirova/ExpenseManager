using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Services;
using ExpenseManager.DBModels;

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