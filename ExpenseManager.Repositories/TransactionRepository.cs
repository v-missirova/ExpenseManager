using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;

namespace ExpenseManager.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<List<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId)
        {
            return FakeStorage.GetTransactions().Where(t => t.WalletId == walletId).ToList();
        }

        public async Task<TransactionDBModel> GetTransactionByIdAsync(Guid id)
        {
            return FakeStorage.GetTransactions().FirstOrDefault(t => t.Id == id);
        }
    }
}