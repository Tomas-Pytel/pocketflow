namespace backend.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; } // always positive and +/- depends on TransactionType
        public string? Description { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // FK for Category
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // FK for User
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
