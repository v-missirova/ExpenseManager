using ExpenseManager.Services;
using ExpenseManagerUIModels;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private readonly IStorageService _storageService;

    public MainPage(IStorageService storageService)
    {
        InitializeComponent();
        _storageService = storageService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        WalletsCollection.ItemsSource = _storageService.GetAllWallets();
    }

    private async void OnWalletSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is WalletUIModel selectedWallet)
        {
            WalletsCollection.SelectedItem = null;

            await Shell.Current.GoToAsync($"WalletDetailsPage?WalletId={selectedWallet.Id}");
        }
    }
}