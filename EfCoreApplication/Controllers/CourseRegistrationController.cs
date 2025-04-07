using EfCoreApplication.Db.AbstractBase;
using EfCoreApplication.Db.EfCore;
using EfCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Controllers
{
    public class CourseRegistrationController : Controller
    {
        private readonly ICourseRegistrationRepository _courseRegistrationRepository;
        private readonly DataContext _context;

        public CourseRegistrationController(ICourseRegistrationRepository courseRegistrationRepository, DataContext context)
        {
            _courseRegistrationRepository = courseRegistrationRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courseRegistrations = await _courseRegistrationRepository.GetAllRegistrationsAsync();
            return View(courseRegistrations);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Students = new SelectList(await _context.Students.ToListAsync(), "StudentId", "NameAndSurname");
            ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRegistration model)
        {
           
                model.RegistrationDate = DateTime.Now;
                await _courseRegistrationRepository.AddRegistrationAsync(model);
                await _courseRegistrationRepository.SaveChangesAsync();
                return RedirectToAction("Index");
            
            //ViewBag.Students = new SelectList(await _context.Students.ToListAsync(), "StudentId", "NameAndSurname");
            //ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "Title");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var registration = await _courseRegistrationRepository.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            ViewBag.Students = new SelectList(await _context.Students.ToListAsync(), "StudentId", "NameAndSurname", registration.StudentId);
            ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "Title", registration.CourseId);

            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseRegistration model)
        {
            if (id != model.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseRegistrationRepository.UpdateRegistrationAsync(model);
                    await _courseRegistrationRepository.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    var registration = await _courseRegistrationRepository.GetRegistrationByIdAsync(id);
                    if (registration == null)
                    {
                        return NotFound();
                    }

                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseRegistrationRepository.DeleteRegistrationAsync(id);
            await _courseRegistrationRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
