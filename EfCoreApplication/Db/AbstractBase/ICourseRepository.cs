using EfCoreApplication.Models;

namespace EfCoreApplication.Db.AbstractBase
{
    public interface ICourseRepository
    {
        IQueryable<Course> Courses { get; }

        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<bool> SaveChangesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
    }
}
