using System;

namespace dotnetapp.Models
{
    public class Admission
    {
        public int AdmissionID { get; set; }
        public DateTime AdmissionDate { get; set; }

        // Foreign keys
        public int StudentId { get; set; }
        public int CourseID { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Course Course { get; set; }

        // Add any additional properties specific to admissions
    }
}
