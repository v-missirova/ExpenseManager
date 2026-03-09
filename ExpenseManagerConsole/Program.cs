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
        private static StorageService _storageService;
        private static List<WalletUIModel> _wallets;

        static void Main(string[] args)
        {
            Console.WriteLine("The best expense manager app!");
            _storageService = new StorageService();
            string? command = null;
            while (_appState != AppState.Exit)
            {
                switch (_appState)
                {
                    case AppState.Default:
                        WalletList(); break;
                    case AppState.ToWallet:
                        WalletInfo(); break;
                    case AppState.ToTransactions:
                        ToTransaction(); break;

                }
                Console.WriteLine("Enter \"Exit\" to exit");
                command = Console.ReadLine();
                UpdateState(command);
            }
        }

        private static void UpdateState(string? command)
        {
            switch (command)
            {
                case "Back":
                    _appState = AppState.Default;
                    break;
                case "Exit":
                    _appState = AppState.Exit;
                    Console.WriteLine("Thank you and see you later!");
                    break;
                default:
                    switch (_appState)
                    {
                        case AppState.Default:
                            _appState = AppState.ToWallet;
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
                Console.WriteLine($"{wallet.ToString()}");
            }
            return;  
        }
        private static void WalletInfo() { return; }
        private static void ToTransaction() { return; }
    }
}