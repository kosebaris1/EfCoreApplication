using EfCoreApplication.Db.AbstractBase;
using EfCoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Db.EfCore
{
    public class EfStudentRepository : IStudentRepository
    {
        private readonly DataContext _Context;

        public EfStudentRepository(DataContext context)
        {
            _Context = context;
        }

        public IQueryable<Student> Students => _Context.Students;

        public async Task AddStudentAsync(Student student)
        {
            await _Context.Students.AddAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _Context.Students.Update(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _Context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            if (student != null)
            {
                _Context.Students.Remove(student);
            }
        }

        public async Task<Student?> GetStudentWithCoursesAsync(int id)
        {
            return await _Context.Students
                .Include(s => s.CourseRegistrations)
                .ThenInclude(cr => cr.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _Context.SaveChangesAsync() > 0;
        }

        public int Count()
        {
            return _Context.Students.Count();
        }

        public int CountByGender(Gender gender)
        {
            return _Context.Students.Count(s => s.Gender == gender);
        }
    }
}
