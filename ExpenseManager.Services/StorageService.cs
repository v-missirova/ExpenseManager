using ExpenseManager.DBModels;
using ExpenseManagerUIModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.Services
{
    public class StorageService
    {
        private List<WalletDBModel> _wallets;
        private List<TransactionDBModel> _transactions;

        public void LoadData()
        {
            if (_wallets != null && _transactions != null) return;

            _wallets = FakeStorage.GetWallets().ToList();
            _transactions = FakeStorage.GetTransactions().ToList();

        }
        public List<WalletUIModel> GetAllWallets()
        { 
            LoadData();
            return _wallets.Select(w => new WalletUIModel(w)).ToList();
        }

        public List<TransactionUIModel> GetTransactionsByWalletId(Guid walletId)
        {
            LoadData();

            List<TransactionUIModel> result = new List<TransactionUIModel>();

            foreach (TransactionDBModel transaction in _transactions)
            {
                if (transaction.WalletId == walletId)
                {
                    result.Add(new TransactionUIModel(transaction));
                }
            }
            return result;
        }


    }
}
