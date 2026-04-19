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

        private Guid _currentWalletId;

        public ObservableCollection<TransactionDBModel> Transactions { get; } = new();

        public string WalletIdString
        {
            set
            {
                if (Guid.TryParse(value, out Guid parsedId))
                {
                    _currentWalletId = parsedId;
                    _ = LoadTransactionsAsync();
                }
            }
        }

        public WalletDetailsViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [RelayCommand]
        public async Task LoadTransactionsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
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