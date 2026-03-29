using ExpenseManagerUIModels;
using System;
using System.Collections.Generic;

namespace ExpenseManager.Services
{
    public interface IStorageService
    {
        void LoadData();
        List<WalletUIModel> GetAllWallets();
        List<TransactionUIModel> GetTransactionsByWalletId(Guid walletId);
    }
}