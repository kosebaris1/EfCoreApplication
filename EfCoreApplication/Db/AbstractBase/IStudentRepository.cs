using EfCoreApplication.Models;
using Microsoft.Extensions.Hosting;

namespace EfCoreApplication.Db.AbstractBase
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<bool> SaveChangesAsync();
        Task<Student?> GetStudentWithCoursesAsync(int id);
        int Count();
        int CountByGender(Gender gender);



    }
}
