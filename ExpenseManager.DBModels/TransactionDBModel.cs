using ExpenseManager.Common.Enums;

namespace ExpenseManager.DBModels {
    /// <summary>
    /// Entity class for storing Transaction data, linked to a specific Wallet via WalletId.
    /// </summary>
    public class TransactionDBModel
    {
        public Guid Id { get; }
        public Guid WalletId { get; }
        public string Description { get; set; }
        public DateTime DataTimeOfTransaction { get;}
        public Category Category { get; set; }
        public decimal Amount { get; set; }

        private TransactionDBModel() { }

        public TransactionDBModel(Guid walletId, decimal amount, Category category, string description) { 

            Id = Guid.NewGuid();
            Description = description;
            Category = category;
            DataTimeOfTransaction = DateTime.Now;   // automatically set timestamp on creation
            WalletId = walletId;             // link to the parent Wallet
            Amount = amount;

        }
        
    }
}
