using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ExpenseManager.Services
{
    internal static class FakeStorage
    {
        private static readonly List<WalletDBModel> _wallets;
        private static readonly List<TransactionDBModel> _transactions;

        internal static IEnumerable<WalletDBModel> GetWallets()
        {
            return _wallets.ToList();
        }
        internal static IEnumerable<TransactionDBModel> GetTransactions() { return _transactions.ToList(); }

        static FakeStorage()
        {
            _wallets = new List<WalletDBModel>();
            _transactions = new List<TransactionDBModel>();

            // 1. Створюємо гаманці
            var wallet1 = new WalletDBModel("mono_uah", Currency.UAH);
            var wallet2 = new WalletDBModel("mono_usd", Currency.USD);
            var wallet3 = new WalletDBModel("privat", Currency.GEL);

            _wallets.Add(wallet1);
            _wallets.Add(wallet2);
            _wallets.Add(wallet3);

            // 2. Створюємо транзакції, використовуючи Id створених гаманців
            // Згідно з вимогами: 10 транзакцій для wallet1
            _transactions.Add(new TransactionDBModel(wallet1.Id, -500m, Category.Groceries, "Сільпо"));
            _transactions.Add(new TransactionDBModel(wallet1.Id, -150m, Category.Cafe, "Кава"));
            // ... (додайте ще 8 транзакцій для wallet1)

            // Згідно з вимогами: 2 транзакції для wallet2
            _transactions.Add(new TransactionDBModel(wallet2.Id, 5000m, Category.Investition, "Зарплата"));
            _transactions.Add(new TransactionDBModel(wallet2.Id, -1000m, Category.Transport, "Бензин"));
        }
    }
}
