using EfCoreApplication.Models;

namespace EfCoreApplication.Db.AbstractBase
{
    public interface ICourseRegistrationRepository
    {
        Task<IEnumerable<CourseRegistration>> GetAllRegistrationsAsync();
        Task<CourseRegistration?> GetRegistrationByIdAsync(int id);
        Task AddRegistrationAsync(CourseRegistration registration);
        Task UpdateRegistrationAsync(CourseRegistration registration);
        Task DeleteRegistrationAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
