using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using ExpenseManager.DTOModels;
using ExpenseManager.Repositories;
using System;
using System.Collections.Generic;

namespace ExpenseManager.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletService(IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<WalletListDTO>> GetAllWalletsAsync()
        {
            var dbWallets = await _walletRepository.GetAllWalletsAsync();
            var result = new List<WalletListDTO>();

            foreach (var dbWallet in dbWallets)
            {
                var dbTransactions = await _transactionRepository.GetTransactionsByWalletIdAsync(dbWallet.Id);
                decimal balance = dbTransactions.Sum(t => t.Amount);

                result.Add(new WalletListDTO
                {
                    Id = dbWallet.Id,
                    Name = dbWallet.Name,
                    Currency = dbWallet.Currency.ToString(),
                    Balance = balance
                });
            }
            return result;
        }

        public async Task<WalletDetailsDTO> GetWalletDetailsAsync(Guid walletId)
        {
            var dbWallet = await _walletRepository.GetWalletByIdAsync(walletId);
            if (dbWallet == null) return null;

            var dbTransactions = await _transactionRepository.GetTransactionsByWalletIdAsync(walletId);

            var transactionDTOs = dbTransactions.Select(t => new TransactionListDTO
            {
                Id = t.Id,
                Category = t.Category.ToString(),
                Description = t.Description,
                Amount = t.Amount,
                DataTimeOfTransaction = t.DateTimeOfTransaction
            }).ToList();

            return new WalletDetailsDTO
            {
                Id = dbWallet.Id,
                Name = dbWallet.Name,
                Currency = dbWallet.Currency.ToString(),
                Balance = dbTransactions.Sum(t => t.Amount),
                Transactions = transactionDTOs
            };
        }
        public async Task<Guid> AddWalletAsync(string name, Currency currency, decimal initialBalance)
        {
            var newWalletId = Guid.NewGuid();

            var newDbWallet = new WalletDBModel
            {
                Id = newWalletId,
                Name = name,
                Currency = currency,
                Balance = initialBalance
            };

            await _walletRepository.SaveWalletAsync(newDbWallet);

            return newWalletId;
        }

        public async Task DeleteWalletAsync(Guid id)
        {
            await _walletRepository.DeleteWalletAsync(id);
        }
    }
}