using Common.Enums;
using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManagerUIModels
{
    public class WalletUIModel
    {
        private WalletDBModel _dbModel;
        private string _name;
        private Currency _currency;
        private List<TransactionUIModel> _transactions;

        // ID is generated once so can't be changed later.
        public Guid Id { get => _dbModel.Id; }
        public string Name { get => _name; set => _name = value; }

        public Currency Currency { get => _currency; set => _currency = value; }
        public IReadOnlyList<TransactionUIModel> Transactions => _transactions;
        public decimal TransactionSum
        {
            get
            {
                if (Transactions == null) return 0;
                decimal sum = 0;
                foreach (var transaction in Transactions) sum += transaction.Amount;
                return sum;
            } set; }

        public WalletUIModel ()
        {
            _transactions = new List<TransactionUIModel>();
        }
        public WalletUIModel (WalletDBModel dbModel) : this() { 
            _dbModel = dbModel;
            _name = dbModel.Name;
            _currency = dbModel.Currency;
        }
    }
}
