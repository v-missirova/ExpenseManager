using ExpenseManager.Services;
using ExpenseManagerUIModels;

namespace MauiApp1;

[QueryProperty(nameof(TransactionIdParam), "TransactionId")]
public partial class TransactionDetailsPage : ContentPage
{
    private readonly IStorageService _storageService;

    public TransactionDetailsPage(IStorageService storageService)
    {
        InitializeComponent();
        _storageService = storageService;
    }

    public string TransactionIdParam
    {
        set
        {
            if (Guid.TryParse(Uri.UnescapeDataString(value), out Guid txId))
            {
                LoadTransactionData(txId);
            }
        }
    }

    private void LoadTransactionData(Guid transactionId)
    {
        var allWallets = _storageService.GetAllWallets();
        TransactionUIModel targetTransaction = null;
        foreach (var wallet in allWallets)
        {
            var transactions = _storageService.GetTransactionsByWalletId(wallet.Id);
            targetTransaction = transactions.FirstOrDefault(t => t.Id == transactionId);

            if (targetTransaction != null)
            {
                break;
            }
        }
        if (targetTransaction != null)
        {
            this.BindingContext = targetTransaction;
        }
    }
}