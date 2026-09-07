namespace backend.Models
{
    public class Saving
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal? Limit { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
