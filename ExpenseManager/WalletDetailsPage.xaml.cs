using ExpenseManager.ViewModels;

namespace ExpenseManager.ExpenseManager;

public partial class WalletDetailsPage : ContentPage
{
    public WalletDetailsPage(WalletDetailsViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}