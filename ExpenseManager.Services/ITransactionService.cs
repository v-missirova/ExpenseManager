using System;
using ExpenseManager.DTOModels;

namespace ExpenseManager.Services
{
    public interface ITransactionService
    {
        Task<DTOModels.TransactionDetailsDTO> GetTransactionDetailsAsync(Guid transactionId);
    }
}