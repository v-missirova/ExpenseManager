using System;
using System.Collections.Generic;
using ExpenseManager.DBModels;
using ExpenseManager.Storage;

namespace ExpenseManager.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IStorageContext _storage;

        public TransactionRepository(IStorageContext storage)
        {
            _storage = storage;
        }

        public async Task<List<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId)
        {

            var transactions = await _storage.GetTransactionsByWalletIdAsync(walletId);
            return transactions.ToList();
        }

        public async Task<TransactionDBModel> GetTransactionByIdAsync(Guid id)
        {
            return await _storage.GetTransactionAsync(id);
        }

        public async Task SaveTransactionAsync(TransactionDBModel transaction)
        {
            await _storage.SaveTransactionAsync(transaction);
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            await _storage.DeleteTransactionAsync(id);
        }
    }
}