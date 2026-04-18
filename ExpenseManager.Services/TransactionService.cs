using System;
using ExpenseManager.Repositories;
using ExpenseManager.DTOModels;

namespace ExpenseManager.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<DTOModels.TransactionDetailsDTO> GetTransactionDetailsAsync(Guid transactionId)
        {
            var dbTransaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (dbTransaction == null) return null;

            return new DTOModels.TransactionDetailsDTO
            {
                Id = dbTransaction.Id,
                Category = dbTransaction.Category.ToString(),
                Description = dbTransaction.Description,
                Amount = dbTransaction.Amount,
                DataTimeOfTransaction = dbTransaction.DataTimeOfTransaction,
                IsExpense = dbTransaction.Amount < 0
            };
        }
    }
}