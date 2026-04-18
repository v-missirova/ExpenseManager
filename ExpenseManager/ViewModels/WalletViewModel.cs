using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.DTO;
using ExpenseManager.Services;

namespace MauiApp1.ViewModels
{
	public partial class WalletViewModel : BaseViewModel
	{
		private readonly IWalletService _walletService;

		public ObservableCollection<WalletListDTO> Wallets { get; } = new();

		[ObservableProperty]
		private WalletListDTO _selectedWallet;

		public MainViewModel(IWalletService walletService)
		{
			_walletService = walletService;
		}

		[RelayCommand]
		private async Task LoadDataAsync()
		{
			var wallets = await _walletService.GetAllWalletsAsync();
			Wallets.Clear();
			foreach (var wallet in wallets)
			{
				Wallets.Add(wallet);
			}
		}

		partial void OnSelectedWalletChanged(WalletListDTO value)
		{
			if (value != null)
			{
				GoToWalletDetailsAsync(value);
				SelectedWallet = null;
			}
		}

		private async void GoToWalletDetailsAsync(WalletListDTO wallet)
		{
			await Shell.Current.GoToAsync($"WalletDetailsPage?WalletId={wallet.Id}");
		}
	}
}