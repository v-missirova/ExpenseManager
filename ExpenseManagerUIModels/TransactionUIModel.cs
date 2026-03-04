using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManagerUIModels
{
    public class TransactionUIModel
    {
        // ID is generated once so can't be changed later.
        public Guid Id { get; }
        // transaction is set to certain wallet so can't be changed later.
        public Guid WalletId { get; }
        public string Description { get; set; }
        // Date and time is set on creation of transaction so can't be changed later.
        public DateTime DataTimeOfTransaction { get; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
    }
    }
