using ExpenseManager.DBModels;
using ExpenseManagerUIModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.Services
{
    /// <summary>
    /// Service layer acting as an intermediary between the UI and the data storage.
    /// Maps DB entities to UI ViewModels.
    /// </summary>
    public class StorageService
    {
        private List<WalletDBModel>? _wallets;
        private List<TransactionDBModel>? _transactions;

        // Caches data from FakeStorage
        public void LoadData()
        {
            if (_wallets != null && _transactions != null) return;

            _wallets = FakeStorage.GetWallets().ToList();
            _transactions = FakeStorage.GetTransactions().ToList();

        }
        public List<WalletUIModel> GetAllWallets()
        {
            if (_wallets == null) LoadData();

            List<WalletUIModel> result = new List<WalletUIModel>();

            foreach (WalletDBModel walletDB in _wallets)
            {
                // map to UI model and inject dependent transactions to calculate the balance
                WalletUIModel walletUI = new WalletUIModel(walletDB);
                List<TransactionUIModel> walletTransactions = GetTransactionsByWalletId(walletDB.Id);
                walletUI.LoadTransactions(walletTransactions);
                result.Add(walletUI);
            }

            return result;
        }

        public List<TransactionUIModel> GetTransactionsByWalletId(Guid walletId)
        {
            LoadData();

            List<TransactionUIModel> result = new List<TransactionUIModel>();

            foreach (TransactionDBModel transaction in _transactions)
            {
                // filter by WalletId and map to UI model
                if (transaction.WalletId == walletId)
                {
                    result.Add(new TransactionUIModel(transaction));
                }
            }
            return result;
        }


    }
}
