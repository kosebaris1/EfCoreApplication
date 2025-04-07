using EfCoreApplication.Db.AbstractBase;
using EfCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.Courses.ToListAsync();
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course model)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.AddCourseAsync(model);
                await _courseRepository.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseRepository.UpdateCourseAsync(course);
                    await _courseRepository.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _courseRepository.Courses.AnyAsync(c => c.CourseId == course.CourseId))
                    {
                        return NotFound();
                    }
                }
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
            await _courseRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
