namespace backend.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ColorHex { get; set; } = string.Empty;

        // FK for User
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
