using ExpenseManager.Common.Enums;

namespace ExpenseManager.DBModels { 

    public class TransactionDBModel
    {
        // ID is generated once so can't be changed later.
        public Guid Id { get; }
       // transaction is set to certain wallet so can't be changed later.
        public Guid WalletId { get; }
        public string Description { get; set; }
        // Date and time is set on creation of transaction so can't be changed later.
        public DateTime DataTimeOfTransaction { get;}
        public Category Category { get; set; }
        public decimal Amount { get; set; }

        private TransactionDBModel() { }

        public TransactionDBModel(Guid walletId, decimal amount, Category category, string description) { 

            Id = Guid.NewGuid();
            Description = description;
            Category = category;
            DataTimeOfTransaction = DateTime.Now;
            WalletId = walletId;
            Amount = amount;

        }
        
    }
}
