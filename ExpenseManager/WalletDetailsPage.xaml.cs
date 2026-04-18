using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class WalletDetailsPage : ContentPage
{
    public WalletDetailsPage(WalletDetailsViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}