using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ExpenseManager.DTOModels;
using ExpenseManager.Services;

namespace MauiApp1.ViewModels
{
    [QueryProperty(nameof(WalletIdParam), "WalletId")]
    public partial class WalletDetailsViewModel : BaseViewModel
    {
        private readonly IWalletService _walletService;

        public string WalletIdParam
        {
            set
            {
                if (Guid.TryParse(Uri.UnescapeDataString(value), out Guid id))
                {
                    LoadWalletDataAsync(id);
                }
            }
        }

        [ObservableProperty]
        public partial WalletDetailsDTO? Wallet { get; set; }

        public ObservableCollection<TransactionListDTO> Transactions { get; } = new();

        [ObservableProperty]
        public partial TransactionListDTO? SelectedTransaction { get; set; }

        public WalletDetailsViewModel(IWalletService walletService)
        {
            _walletService = walletService;
        }

        private async void LoadWalletDataAsync(Guid id)
        {
            Wallet = await _walletService.GetWalletDetailsAsync(id);

            if (Wallet != null && Wallet.Transactions != null)
            {
                Transactions.Clear();
                foreach (var tx in Wallet.Transactions)
                {
                    Transactions.Add(tx);
                }
            }
        }

        partial void OnSelectedTransactionChanged(TransactionListDTO? value)
        {
            if (value != null)
            {
                GoToTransactionDetailsAsync(value);
                SelectedTransaction = null;
            }
        }

        private async void GoToTransactionDetailsAsync(TransactionListDTO tx)
        {
            await Shell.Current.GoToAsync($"TransactionDetailsPage?TransactionId={tx.Id}");
        }
    }
}