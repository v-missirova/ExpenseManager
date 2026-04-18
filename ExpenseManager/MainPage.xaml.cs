using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage(WalletViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is WalletViewModel vm)
        {
            vm.LoadDataCommand.Execute(null);
        }
    }
}