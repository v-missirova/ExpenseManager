using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.DBModels
{
    public class WalletDBModel
    {
       // ID is generated once so can't be changed later.
        public Guid Id { get; }
        public string Name { get; set; } 

        public Currency Currency { get; set; }

        private WalletDBModel() { }
        public WalletDBModel(string name, Currency currency)
        {
            Id = Guid.NewGuid();
            Name = name;
            Currency = currency;

        }

    }
}
