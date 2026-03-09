using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManagerUIModels
{
    public class WalletUIModel
    {
        private readonly List<TransactionUIModel> _transactions;
        public Guid Id { get; }
        public string Name { get; set; }
        public Currency Currency { get; set; }

        public IReadOnlyList<TransactionUIModel> Transactions => _transactions;

        public decimal TransactionSum => _transactions.Sum(t => t.Amount);

        public WalletUIModel(WalletDBModel dbModel)
        {
            Id = dbModel.Id;
            Name = dbModel.Name;
            Currency = dbModel.Currency;
            _transactions = new List<TransactionUIModel>();
        }

        public override string ToString()
        {
            return $"Wallet: {Name}, {Currency}"; ;
        }
    }
}