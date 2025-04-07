using EfCoreApplication.Db.AbstractBase;
using EfCoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Db.EfCore
{
    public class EfCourseRegistrationRepository : ICourseRegistrationRepository
    {
        private readonly DataContext _context;

        public EfCourseRegistrationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseRegistration>> GetAllRegistrationsAsync()
        {
            return await _context.CourseRegistrations
                .Include(x => x.Student)
                .Include(x => x.Course)
                .ToListAsync();
        }

        public async Task<CourseRegistration?> GetRegistrationByIdAsync(int id)
        {
            return await _context.CourseRegistrations
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(cr => cr.RegistrationId == id);
        }

        public async Task AddRegistrationAsync(CourseRegistration registration)
        {
            await _context.CourseRegistrations.AddAsync(registration);
        }

        public async Task UpdateRegistrationAsync(CourseRegistration registration)
        {
            _context.CourseRegistrations.Update(registration);
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            var registration = await GetRegistrationByIdAsync(id);
            if (registration != null)
            {
                _context.CourseRegistrations.Remove(registration);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
