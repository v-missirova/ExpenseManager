using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExpenseManagerUIModels
{
    public class TransactionUIModel
    {
        // Id is created once, no need for setter
        public Guid Id { get; }
        public Guid WalletId { get; }
        public string Description { get; set; }
        // Data and time is set once, no need for setter
        public DateTime DateTimeOfTransaction { get; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public bool IsExpense => Amount < 0;

        public TransactionUIModel(TransactionDBModel dbModel)
        {
            Id = dbModel.Id;
            WalletId = dbModel.WalletId;
            Category = dbModel.Category;
            Description = dbModel.Description;
            Amount = dbModel.Amount;
            DateTimeOfTransaction = dbModel.DataTimeOfTransaction;
        }
    }
}