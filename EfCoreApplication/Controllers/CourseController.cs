using EfCoreApplication.Db;
using EfCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course model)
        {
            _context.Courses.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var ogr= await _context.Ogrenciler.FindAsync(id);
            var course = await _context.Courses.FirstOrDefaultAsync(o => o.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Courses.Update(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(o => o.CourseId == course.CourseId))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(course);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
