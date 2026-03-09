using ExpenseManager.Common.Enums;
using ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExpenseManagerUIModels
{
    /// <summary>
    /// View model for displaying Transaction data.
    /// </summary>
    public class TransactionUIModel
    {
        public Guid Id { get; }
        public Guid WalletId { get; }
        public string Description { get; set; }
        public DateTime DateTimeOfTransaction { get; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }

        // computed field determining if the transaction is an expense
        public bool IsExpense => Amount < 0;

        // mapping data from DB entity to UI model
        public TransactionUIModel(TransactionDBModel dbModel)
        {
            Id = dbModel.Id;
            WalletId = dbModel.WalletId;
            Category = dbModel.Category;
            Description = dbModel.Description;
            Amount = dbModel.Amount;
            DateTimeOfTransaction = dbModel.DataTimeOfTransaction;
        }
        public override string ToString()
        {
            return $"- [{DateTimeOfTransaction:g}] {Category} | {Description}: {Amount} | (IsExpense?): {IsExpense})";
        }
    }
}