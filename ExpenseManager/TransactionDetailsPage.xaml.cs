using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class TransactionDetailsPage : ContentPage
{
    public TransactionDetailsPage(TransactionDetailsViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}