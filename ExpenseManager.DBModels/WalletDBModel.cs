using ExpenseManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.DBModels
{
    /// <summary>
    /// Entity class for storing Wallet data.
    /// </summary>
    public class WalletDBModel
    {
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
