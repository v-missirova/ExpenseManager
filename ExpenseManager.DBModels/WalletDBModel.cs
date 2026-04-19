using ExpenseManager.Common.Enums;
using SQLite;
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
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }
        public WalletDBModel()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
        }
    }
}
