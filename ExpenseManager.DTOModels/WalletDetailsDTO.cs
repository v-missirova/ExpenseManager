using System;
using System.Collections.Generic;

namespace ExpenseManager.Services.DTOModels
{
    public class WalletDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Currency { get;}
        public decimal Balance { get;}

        public List<TransactionListDTO> Transactions { get; set; }
    }
}