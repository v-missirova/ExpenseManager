using System;
using System.Collections.Generic;

namespace ExpenseManager.DTOModels
{
    public class WalletDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }

        public List<TransactionListDTO> Transactions { get; set; }
    }
}