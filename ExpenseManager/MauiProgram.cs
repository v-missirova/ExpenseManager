using Microsoft.Extensions.Logging;
using ExpenseManager.Repositories;
using ExpenseManager.Services;

namespace ExpenseManager.ExpenseManager;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // registration of Repositories
        builder.Services.AddSingleton<IWalletRepository, WalletRepository>();
        builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();

        // registration of Services
        builder.Services.AddTransient<IWalletService, WalletService>();
        builder.Services.AddTransient<ITransactionService, TransactionService>();

        // registration of ViewModels
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<WalletDetailsViewModel>();
        builder.Services.AddTransient<TransactionDetailsViewModel>();

        // registation of pages
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<WalletDetailsPage>();
        builder.Services.AddTransient<TransactionDetailsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}