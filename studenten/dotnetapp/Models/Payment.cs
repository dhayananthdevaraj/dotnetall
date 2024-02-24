using System;

namespace dotnetapp.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }

        // Foreign keys
        public int StudentId { get; set; }
        public int CourseID { get; set; }
        public int AdmissionID { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Admission Admission { get; set; }

        // Add any additional properties specific to payments
    }
}
