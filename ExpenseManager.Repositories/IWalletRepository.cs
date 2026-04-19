using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;

namespace ExpenseManager.Repositories
{
    public interface IWalletRepository
    {
        Task<List<WalletDBModel>> GetAllWalletsAsync();
        Task<WalletDBModel> GetWalletByIdAsync(Guid id);
        Task SaveWalletAsync(WalletDBModel wallet);
        Task DeleteWalletAsync(Guid id);
    }
}