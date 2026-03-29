namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("WalletDetailsPage", typeof(WalletDetailsPage));
            Routing.RegisterRoute("TransactionDetailsPage", typeof(TransactionDetailsPage));
        }
    }
}
