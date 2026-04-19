using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class WalletDetailsPage : ContentPage
{
    public WalletDetailsPage(WalletDetailsViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}