using ExpenseManager.Common.Enums;
using SQLite;
namespace ExpenseManager.DBModels
{
    /// <summary>
    /// Entity class for storing Transaction data, linked to a specific Wallet via WalletId.
    /// </summary>
    public class TransactionDBModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Indexed]
        public Guid WalletId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTimeOfTransaction { get; set; }

    }
}
