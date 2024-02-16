namespace dotnetapp.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public long UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }

        // Navigation Property: Review belongs to one user
        public User? User { get; set; }
    }
}
