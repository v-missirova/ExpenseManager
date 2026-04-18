using System;

namespace ExpenseManager.DTOModels
{
	public class WalletListDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Currency { get; set; }

		public decimal Balance { get; set;  }
	}
}