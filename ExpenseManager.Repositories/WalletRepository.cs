using System;
using System.Collections.Generic;
using ExpenseManager.DBModels;
using ExpenseManager.Storage;

namespace ExpenseManager.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IStorageContext _storage;
        public WalletRepository(IStorageContext storage)
        {
            _storage = storage;
        }
        public async Task UpdateWalletAsync(WalletDBModel wallet)
        {
            await _storage.DeleteWalletAsync(wallet.Id);

            await _storage.SaveWalletAsync(wallet);
        }
        public async Task<List<WalletDBModel>> GetAllWalletsAsync()
        {
            var wallets = new List<WalletDBModel>();
            await foreach (var wallet in _storage.GetWalletsAsync())
            {
                wallets.Add(wallet);
            }
            return wallets;
        }

        public async Task<WalletDBModel> GetWalletByIdAsync(Guid id)
        {
            return await _storage.GetWalletAsync(id);
        }

        public async Task SaveWalletAsync(WalletDBModel wallet)
        {
            await _storage.SaveWalletAsync(wallet);
        }

        public async Task DeleteWalletAsync(Guid id)
        {
            await _storage.DeleteWalletAsync(id);
        }
    }
}