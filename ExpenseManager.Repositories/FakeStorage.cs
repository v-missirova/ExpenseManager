using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ExpenseManager.Repositories
{
    /// <summary>
    /// Static fake storage. Internal (resctricted) acces ensures that only Services can access it.
    /// </summary>
    internal static class FakeStorage
    {
        private static readonly List<WalletDBModel> _wallets;
        private static readonly List<TransactionDBModel> _transactions;

        internal static IEnumerable<WalletDBModel> GetWallets()
        {
            return _wallets.ToList();
        }
        internal static IEnumerable<TransactionDBModel> GetTransactions() { return _transactions.ToList(); }

        // Static constructor initializes seed data once upon first access
        static FakeStorage()
        {
            _wallets = new List<WalletDBModel>();
            _transactions = new List<TransactionDBModel>();

            var wallet1 = new WalletDBModel("mono_uah", Currency.UAH);
            var wallet2 = new WalletDBModel("mono_usd", Currency.USD);
            var wallet3 = new WalletDBModel("privat", Currency.GEL);

            _wallets.Add(wallet1);
            _wallets.Add(wallet2);
            _wallets.Add(wallet3);

            _transactions.Add(new TransactionDBModel(wallet1.Id, -500m, Category.Groceries, "silpo"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -150m, Category.Cafe, "coffee"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -250m, Category.Other, "rozetka"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -80m, Category.Transport, "taxi"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, 15000m, Category.Investition, "salary"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -450m, Category.Cafe, "Puzata Hata"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -1200m, Category.Cafe, "Lviv Croissant"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -350m, Category.Transport, "taxi"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, 2000m, Category.Other, "flowers"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -90m, Category.Cafe, "silpo"));

            _transactions.Add(new TransactionDBModel(wallet2.Id, 5000m, Category.Investition, "salary"));
            _transactions.Add(new TransactionDBModel(wallet2.Id, -1000m, Category.Transport, "taxi"));
        }
    }
}
