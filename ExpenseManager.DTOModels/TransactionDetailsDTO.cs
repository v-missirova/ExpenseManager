using System;

namespace ExpenseManager.DTOModels
{
    public class TransactionDetailsDTO
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTimeOfTransaction { get; set; }

        public bool IsExpense { get; set; }
    }
}