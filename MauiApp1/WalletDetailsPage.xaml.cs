using ExpenseManager.Services;
using ExpenseManagerUIModels;

namespace MauiApp1;

[QueryProperty(nameof(WalletIdParam), "WalletId")]
public partial class WalletDetailsPage : ContentPage
{
    private readonly IStorageService _storageService;
    private WalletUIModel _currentWallet;

    public WalletDetailsPage(IStorageService storageService)
    {
        InitializeComponent();
        _storageService = storageService;
    }
    public string WalletIdParam
    {
        set
        {
            if (Guid.TryParse(Uri.UnescapeDataString(value), out Guid walletId))
            {
                LoadWalletData(walletId);
            }
        }
    }

    private void LoadWalletData(Guid walletId)
    {
        _currentWallet = _storageService.GetAllWallets().FirstOrDefault(w => w.Id == walletId);

        if (_currentWallet != null)
        {
            var transactions = _storageService.GetTransactionsByWalletId(walletId);
            _currentWallet?.LoadTransactions(transactions);
            this.BindingContext = _currentWallet;
            TransactionsCollection.ItemsSource = _currentWallet?.Transactions;
        }
    }

    private async void OnTransactionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is TransactionUIModel selectedTransaction)
        {
            TransactionsCollection.SelectedItem = null;
            await Shell.Current.GoToAsync($"TransactionDetailsPage?TransactionId={selectedTransaction.Id}");
        }
    }
}