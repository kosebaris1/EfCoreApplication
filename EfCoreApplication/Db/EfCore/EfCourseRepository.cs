using EfCoreApplication.Db.AbstractBase;
using EfCoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Db.EfCore
{
    public class EfCourseRepository : ICourseRepository
    {
        private readonly DataContext _Context;

        public EfCourseRepository(DataContext context)
        {
            _Context = context;
        }

        public IQueryable<Course> Courses => _Context.Courses;

        public async Task AddCourseAsync(Course course)
        {
            await _Context.Courses.AddAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _Context.Courses.Update(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _Context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (course != null)
            {
                _Context.Courses.Remove(course);
            }
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _Context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _Context.SaveChangesAsync() > 0;
        }
    
     }
}
