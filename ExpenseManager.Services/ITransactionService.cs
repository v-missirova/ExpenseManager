using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using ExpenseManager.DTOModels;
using System;

namespace ExpenseManager.Services
{
    public interface ITransactionService
    {
        Task<DTOModels.TransactionDetailsDTO> GetTransactionDetailsAsync(Guid transactionId);
        Task<List<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId);
        Task AddTransactionAsync(Guid walletId, decimal amount, Category category, string description);
        Task DeleteTransactionAsync(Guid transactionId, Guid walletId);
    }

}