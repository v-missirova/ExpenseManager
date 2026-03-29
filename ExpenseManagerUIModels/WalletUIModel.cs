    using ExpenseManager.Common.Enums;
    using ExpenseManager.DBModels;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace ExpenseManagerUIModels
    {
        /// <summary>
        /// View model for displaying Wallet data.
        /// </summary>
        public class WalletUIModel
        {
            private readonly List<TransactionUIModel> _transactions;
            public Guid Id { get; }
            public string Name { get; set; }
            public Currency Currency { get; set; }

            public IReadOnlyList<TransactionUIModel> Transactions => _transactions;

            // computed field calculating the sum of all loaded transactions
            public decimal TransactionSum => _transactions.Sum(t => t.Amount);

            /// <summary>
            /// sets the wallet with transactions to allow balance calculation.
            /// </summary>
            public void LoadTransactions(List<TransactionUIModel> transactions)
            {
                _transactions.Clear();
                _transactions.AddRange(transactions);
            }

            // mapping data from DB entity to UImodel
            public WalletUIModel(WalletDBModel dbModel)
            {
                Id = dbModel.Id;
                Name = dbModel.Name;
                Currency = dbModel.Currency;
                _transactions = new List<TransactionUIModel>();
            }

            public override string ToString()
            {
                return $"Wallet: {Name}, {Currency}, balance: {TransactionSum}"; ;
            }
        }
    }