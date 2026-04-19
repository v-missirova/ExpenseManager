using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.DTOModels;
using ExpenseManager.Services;
using ExpenseManager.Common.Enums;

namespace MauiApp1.ViewModels
{
    public partial class WalletViewModel : BaseViewModel
    {
        private readonly IWalletService _walletService;

        public ObservableCollection<WalletListDTO> Wallets { get; } = new();

        [ObservableProperty]
        public partial WalletListDTO? SelectedWallet { get; set; }
        [ObservableProperty]
        public partial string NewWalletName { get; set; }
        [ObservableProperty]
        public partial decimal NewWalletBalance { get; set; }

        [ObservableProperty]
        public partial Currency NewWalletCurrency { get; set; }
        [ObservableProperty]
        public partial bool IsAddFormVisible { get; set; }

        public List<Currency> Currencies { get; } = Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();

        public WalletViewModel(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await Task.Delay(800);

                var wallets = await _walletService.GetAllWalletsAsync();
                Wallets.Clear();
                foreach (var wallet in wallets)
                {
                    Wallets.Add(wallet);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        private void ToggleAddForm()
        {
            IsAddFormVisible = !IsAddFormVisible;
        }
        [RelayCommand]
        private async Task AddWalletAsync()
        {
            if (string.IsNullOrWhiteSpace(NewWalletName)) return;

            var nameToSave = NewWalletName;
            var currencyToSave = NewWalletCurrency;
            var balanceToSave = NewWalletBalance;
            IsBusy = true;

            var createdId = await _walletService.AddWalletAsync(nameToSave, currencyToSave, balanceToSave);

            Wallets.Add(new WalletListDTO
            {
                Id = createdId,
                Name = nameToSave,
                Currency = currencyToSave.ToString(),
                Balance = balanceToSave
            });

            NewWalletName = string.Empty;
            NewWalletBalance = 0;
            IsAddFormVisible = false;
            IsBusy = false;
        }

        [RelayCommand]
        private async Task DeleteWalletAsync(WalletListDTO wallet)
        {
            if (wallet == null) return;

            bool confirm = await Shell.Current.DisplayAlert("confirm", $"do you insist " +
                $"on deleting the wallet '{wallet.Name}'" +
                $" and all of it transactions", "yes", "no");
            if (!confirm) return;

            IsBusy = true;

            await _walletService.DeleteWalletAsync(wallet.Id);
            Wallets.Remove(wallet);

            IsBusy = false;
        }

        partial void OnSelectedWalletChanged(WalletListDTO? value)
        {
            if (value != null)
            {
                GoToWalletDetailsAsync(value);
                SelectedWallet = null;
            }
        }

        private async void GoToWalletDetailsAsync(WalletListDTO wallet)
        {
            await Shell.Current.GoToAsync($"WalletDetailsPage?WalletId={wallet.Id.ToString()}");
        }
    }
}