
namespace dotnetapp.Models
{
    public class Job
    {
        public int JobId { get; set; } // Assuming you have a custom ObjectId type
        public int UserId { get; set; } // Assuming you have a custom ObjectId type
        public string Title { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; } // Assuming this is a string for URL or file path

    }
}
