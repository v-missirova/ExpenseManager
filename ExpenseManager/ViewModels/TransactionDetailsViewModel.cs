using System;
using CommunityToolkit.Mvvm.ComponentModel;
using ExpenseManager.DTO;
using ExpenseManager.Services;

namespace MauiApp1.ViewModels
{
    [QueryProperty(nameof(TransactionIdParam), "TransactionId")]
    public partial class TransactionDetailsViewModel : BaseViewModel
    {
        private readonly ITransactionService _transactionService;

        [ObservableProperty]
        private TransactionDetailsDTO _transaction;

        public string TransactionIdParam
        {
            set
            {
                if (Guid.TryParse(Uri.UnescapeDataString(value), out Guid id))
                {
                    LoadTransactionDataAsync(id);
                }
            }
        }

        public TransactionDetailsViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        private async void LoadTransactionDataAsync(Guid id)
        {
            Transaction = await _transactionService.GetTransactionDetailsAsync(id);
        }
    }
}