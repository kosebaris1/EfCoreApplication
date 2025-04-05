using System.ComponentModel.DataAnnotations;

namespace EfCoreApplication.Models
{
    public class CourseRegistration
    {
        [Key]
        public int RegistrationId { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; } = null!;

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;

        public DateTime RegistrationDate { get; set; }
    }
}
