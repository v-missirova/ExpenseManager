using ExpenseManager.Services;
using ExpenseManagerUIModels;
using System.Net.NetworkInformation;

namespace ExpenseManagerConsole
{
    internal class Program
    {
        enum AppState
        {
            Default,
            ToWallet,
            ToTransactions,
            Back,
            Exit
        }

        private static AppState _appState = AppState.Default;
        private static StorageService? _storageService;
        private static List<WalletUIModel>? _wallets;
        private static WalletUIModel? _selectedWallet;

        static void Main(string[] args)
        {
            Console.WriteLine("The best expense manager app!");
            _storageService = new StorageService();
            string? command;
            while (_appState != AppState.Exit)
            {
                switch (_appState)
                {
                    case AppState.Default:
                        WalletList(); break;
                    case AppState.ToWallet:
                        WalletInfo(); break;

                }
                Console.WriteLine("\nEnter \"Exit\" to exit. Enter \"Refresh\" to refresh data.");
                command = Console.ReadLine()?.ToLower();
                UpdateState(command);
            }
        }

        private static void UpdateState(string? command) // global commands handler 
        {
            switch (command)
            {
                case "back":
                    _appState = AppState.Default;
                    Console.Clear();
                    break;

                case "exit":
                    _appState = AppState.Exit;
                    Console.WriteLine("Thank you and see you later. :)");
                    break;

                case "refresh":
                    _wallets = null; // forces data reload on next show
                    _appState = AppState.Default;
                    Console.Clear();
                    Console.WriteLine("Data has been refreshed successfully.");
                    break;
                default:
                    switch (_appState)
                    {
                        case AppState.Default:
                            _selectedWallet = _wallets?.FirstOrDefault(w => w.Name == command);
                            if (_selectedWallet != null)
                            {
                                _appState = AppState.ToWallet;
                            }
                            else
                            {
                                Console.WriteLine($"Wallet with name '{command}' not found. Press Enter.");
                                Console.ReadLine();
                            }
                            break;
                        case AppState.Exit:
                            Console.WriteLine("Unknown command. Please try again.");
                            break;
                    }
                    break;
            }
        }
        private static void LoadWallets()
        {
            if (_wallets != null)
                return;
            _wallets = _storageService.GetAllWallets();
        }
        private static void WalletList() {
            Console.WriteLine("List of wallets:");
            LoadWallets();
            foreach (var wallet in _wallets)
            {
                Console.WriteLine($"{wallet}");
            }
            Console.WriteLine("Enter name of the wallet to view its transactions.");
            return;  
        }
        private static void WalletInfo() {
            Console.WriteLine($"\n~~~ Transactions for {_selectedWallet.Name} ~~~");

            var transactions = _storageService?.GetTransactionsByWalletId(_selectedWallet.Id);

            if (transactions?.Count == 0)
            {
                Console.WriteLine("No transactions found in this wallet.");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
        }
    }
}