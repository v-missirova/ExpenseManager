using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using ExpenseManager.DTOModels;
using ExpenseManager.Repositories;

namespace ExpenseManager.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;

        public TransactionService(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<List<TransactionDBModel>> GetTransactionsByWalletIdAsync(Guid walletId)
        {
            var transactions = await _transactionRepository.GetTransactionsByWalletIdAsync(walletId);
            return transactions.OrderByDescending(t => t.DateTimeOfTransaction).ToList();
        }
        public async Task<TransactionDetailsDTO> GetTransactionDetailsAsync(Guid transactionId)
        {
            var dbTransaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);

            if (dbTransaction == null)
                return null;

            return new TransactionDetailsDTO
            {
                Id = dbTransaction.Id,
                Amount = dbTransaction.Amount,
                Category = dbTransaction.Category.ToString(),
                Description = dbTransaction.Description,
                DateTimeOfTransaction = dbTransaction.DateTimeOfTransaction

                // WalletId = dbTransaction.WalletId 
            };
        }
        public async Task AddTransactionAsync(Guid walletId, decimal amount, Category category, string description)
        {
            var newTransaction = new TransactionDBModel
            {
                Id = Guid.NewGuid(),
                WalletId = walletId,
                Amount = amount,
                Category = category,
                Description = description,
                DateTimeOfTransaction = DateTime.Now
            };
            await _transactionRepository.SaveTransactionAsync(newTransaction);

            var wallet = await _walletRepository.GetWalletByIdAsync(walletId);
            if (wallet != null)
            {
                wallet.Balance += amount;
                await _walletRepository.UpdateWalletAsync(wallet);
            }
        }

        public async Task DeleteTransactionAsync(Guid transactionId, Guid walletId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction != null)
            {
                var wallet = await _walletRepository.GetWalletByIdAsync(walletId);
                if (wallet != null)
                {
                    wallet.Balance -= transaction.Amount;
                    await _walletRepository.UpdateWalletAsync(wallet);
                }
                await _transactionRepository.DeleteTransactionAsync(transactionId);
            }
        }
    }
}