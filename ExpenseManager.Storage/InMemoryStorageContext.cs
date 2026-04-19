using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq; // ДОДАНО, щоб працювали методи FirstOrDefault та Where
using System.Threading.Tasks;

namespace ExpenseManager.Storage
{
    public class InMemoryStorageContext : IStorageContext
    {
        private readonly List<WalletDBModel> _wallets = new();
        private readonly List<TransactionDBModel> _transactions = new();

        public InMemoryStorageContext()
        {
            var wallet1 = new WalletDBModel { Id = Guid.NewGuid(), Name = "mono_uah", Currency = Currency.UAH, Balance = 0 };
            var wallet2 = new WalletDBModel { Id = Guid.NewGuid(), Name = "mono_usd", Currency = Currency.USD, Balance = 0 };
            var wallet3 = new WalletDBModel { Id = Guid.NewGuid(), Name = "privat", Currency = Currency.GEL, Balance = 0 };

            _wallets.Add(wallet1);
            _wallets.Add(wallet2);
            _wallets.Add(wallet3);

            _transactions.Add(new TransactionDBModel { Id = Guid.NewGuid(), WalletId = wallet1.Id, Amount = -500m, Category = Category.Groceries, Description = "silpo", DateTimeOfTransaction = DateTime.Now });
            _transactions.Add(new TransactionDBModel { Id = Guid.NewGuid(), WalletId = wallet1.Id, Amount = -150m, Category = Category.Cafe, Description = "coffee", DateTimeOfTransaction = DateTime.Now });
            _transactions.Add(new TransactionDBModel { Id = Guid.NewGuid(), WalletId = wallet1.Id, Amount = -250m, Category = Category.Other, Description = "rozetka", DateTimeOfTransaction = DateTime.Now });
            _transactions.Add(new TransactionDBModel { Id = Guid.NewGuid(), WalletId = wallet1.Id, Amount = -80m, Category = Category.Transport, Description = "taxi", DateTimeOfTransaction = DateTime.Now });
            _transactions.Add(new TransactionDBModel { Id = Guid.NewGuid(), WalletId = wallet1.Id, Amount = 15000m, Category = Category.Investition, Description = "salary", DateTimeOfTransaction = DateTime.Now });

            _transactions.Add(new TransactionDBModel { Id = Guid.NewGuid(), WalletId = wallet2.Id, Amount = 5000m, Category = Category.Investition, Description = "salary", DateTimeOfTransaction = DateTime.Now });
            _transactions.Add(new TransactionDBModel { Id = Guid.NewGuid(), WalletId = wallet2.Id, Amount = -1000m, Category = Category.Transport, Description = "taxi", DateTimeOfTransaction = DateTime.Now });
        }

#pragma warning disable CS1998

        // ДОДАНО: Порожній метод ініціалізації для виконання контракту інтерфейсу
        public async Task InitAsync()
        {
            // Для бази в пам'яті нічого ініціалізувати не потрібно
        }

        public async IAsyncEnumerable<WalletDBModel> GetWalletsAsync()
        {
            foreach (var wallet in _wallets)
                yield return wallet;
        }

        public async Task<WalletDBModel> GetWalletAsync(Guid walletId) => _wallets.FirstOrDefault(w => w.Id == walletId);
        public async Task<IEnumerable<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId) => _transactions.Where(t => t.WalletId == walletId).ToList();
        public async Task<TransactionDBModel> GetTransactionAsync(Guid transactionId) => _transactions.FirstOrDefault(t => t.Id == transactionId);
        public async Task<int> GetTransactionsCountByWalletIdAsync(Guid walletId) => _transactions.Count(t => t.WalletId == walletId);
        public async Task SaveWalletAsync(WalletDBModel wallet) { _wallets.Add(wallet); }
        public async Task DeleteWalletAsync(Guid walletId) { _wallets.RemoveAll(w => w.Id == walletId); }
        public async Task SaveTransactionAsync(TransactionDBModel transaction) { _transactions.Add(transaction); }
        public async Task DeleteTransactionAsync(Guid transactionId) { _transactions.RemoveAll(t => t.Id == transactionId); }

#pragma warning restore CS1998
    }
}