using System;
using System.Collections.Generic;
using ExpenseManager.DBModels;
using SQLite;

namespace ExpenseManager.Storage
{
    public class SQLLiteStorageContext : IStorageContext
    {
        private const string DatabaseFileName = "expense_manager.db3";
        private static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB Storage 1", DatabaseFileName);
        private SQLiteAsyncConnection _databaseConnection;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        // ЗМІНЕНО: тепер public і називається InitAsync
        public async Task InitAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                if (_databaseConnection is not null)
                    return;

                bool isFirstLaunch = !File.Exists(DatabasePath);

                if (isFirstLaunch)
                    await CreateMockStorage();
                else
                    _databaseConnection = new SQLiteAsyncConnection(DatabasePath);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task CreateMockStorage()
        {
            var dir = Path.GetDirectoryName(DatabasePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            _databaseConnection = new SQLiteAsyncConnection(DatabasePath);

            await _databaseConnection.CreateTableAsync<WalletDBModel>();
            await _databaseConnection.CreateTableAsync<TransactionDBModel>();

            var inMemoryStorage = new InMemoryStorageContext();

            await foreach (var wallet in inMemoryStorage.GetWalletsAsync())
            {
                await _databaseConnection.InsertAsync(wallet);
                await _databaseConnection.InsertAllAsync(await inMemoryStorage.GetTransactionsByWalletIdAsync(wallet.Id));
            }
        }

        public async IAsyncEnumerable<WalletDBModel> GetWalletsAsync()
        {
            await InitAsync();
            foreach (var wallet in await _databaseConnection.Table<WalletDBModel>().ToListAsync())
            {
                yield return wallet;
            }
        }

        public async Task<WalletDBModel> GetWalletAsync(Guid walletId)
        {
            await InitAsync();
            return await _databaseConnection.Table<WalletDBModel>().FirstOrDefaultAsync(w => w.Id == walletId);
        }

        public async Task<IEnumerable<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId)
        {
            await InitAsync();
            return await _databaseConnection.Table<TransactionDBModel>().Where(t => t.WalletId == walletId).ToListAsync();
        }

        public async Task<TransactionDBModel> GetTransactionAsync(Guid transactionId)
        {
            await InitAsync();
            return await _databaseConnection.Table<TransactionDBModel>().FirstOrDefaultAsync(t => t.Id == transactionId);
        }

        public async Task<int> GetTransactionsCountByWalletIdAsync(Guid walletId)
        {
            await InitAsync();
            return await _databaseConnection.Table<TransactionDBModel>().CountAsync(t => t.WalletId == walletId);
        }

        public async Task SaveWalletAsync(WalletDBModel wallet)
        {
            await InitAsync();
            if (await GetWalletAsync(wallet.Id) == null)
                await _databaseConnection.InsertAsync(wallet);
            else
                await _databaseConnection.UpdateAsync(wallet);
        }

        public async Task DeleteWalletAsync(Guid walletId)
        {
            await InitAsync();
            await _databaseConnection.DeleteAsync<WalletDBModel>(walletId);
            var transactions = await GetTransactionsByWalletIdAsync(walletId);
            foreach (var t in transactions)
            {
                await _databaseConnection.DeleteAsync<TransactionDBModel>(t.Id);
            }
        }

        public async Task SaveTransactionAsync(TransactionDBModel transaction)
        {
            await InitAsync();
            if (await GetTransactionAsync(transaction.Id) == null)
                await _databaseConnection.InsertAsync(transaction);
            else
                await _databaseConnection.UpdateAsync(transaction);
        }

        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            await InitAsync();
            await _databaseConnection.DeleteAsync<TransactionDBModel>(transactionId);
        }
    }
}