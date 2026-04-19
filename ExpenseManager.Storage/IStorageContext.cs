using System;
using System.Collections.Generic;
using ExpenseManager.DBModels;

namespace ExpenseManager.Storage
{
    public interface IStorageContext
    {
        // for wallets
        IAsyncEnumerable<WalletDBModel> GetWalletsAsync();
        Task<WalletDBModel> GetWalletAsync(Guid walletId);
        Task SaveWalletAsync(WalletDBModel wallet);
        Task DeleteWalletAsync(Guid walletId);

        // for transactions
        Task<IEnumerable<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId);
        Task<TransactionDBModel> GetTransactionAsync(Guid transactionId);
        Task<int> GetTransactionsCountByWalletIdAsync(Guid walletId);
        Task SaveTransactionAsync(TransactionDBModel transaction);
        Task DeleteTransactionAsync(Guid transactionId);
        Task InitAsync();
    }
}