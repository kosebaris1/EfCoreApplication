using System.ComponentModel.DataAnnotations;

namespace EfCoreApplication.Models
{
    public class Student
    {
        [Display(Name = "Ögrenci Id")]
        public int StudentId { get; set; } //içinde Id kelimesi geçtiği için sistem otomatik olarak primary key olarak atıyor örnek olarak tc kimlik numarasının bensersiz olmasını istiyorsak üstüne [key] yazmak gerekiyor.

        [Display(Name = "Ögrenci Adı")]
        
        public string? StudentName { get; set; } 

        [Display(Name = "Ögrenci Soyad")]
        public string? StudentSurname { get; set; }

        public string? NameAndSurname {
            get
            {
                return this.StudentName+" "+this.StudentSurname;
            }
        }

        [EmailAddress]
        public string? Email { get; set; }

    
        public string? Phone { get; set; }

        [Required]
        public Gender Gender { get; set;}

        public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();
    }

    public enum Gender
    {
        erkek=1,
        kadın=2
    }
}
