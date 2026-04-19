using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;

namespace ExpenseManager.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId);
        Task<TransactionDBModel> GetTransactionByIdAsync(Guid id);
        Task SaveTransactionAsync(TransactionDBModel transaction);
        Task DeleteTransactionAsync(Guid id);
    }
}