using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;

namespace ExpenseManager.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        public async Task<List<WalletDBModel>> GetAllWalletsAsync()
        {
            return FakeStorage.GetWallets().ToList();
        }

        public async Task<WalletDBModel> GetWalletByIdAsync(Guid id)
        {
            return FakeStorage.GetWallets().FirstOrDefault(w => w.Id == id);
        }
    }
}