using EfCoreApplication.Db.AbstractBase;
using EfCoreApplication.Db.EfCore;
using EfCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _context;
        public StudentController(IStudentRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            await _context.AddStudentAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ogr = await _context.GetStudentWithCoursesAsync(id.Value);

            if (ogr == null)
                return NotFound();

            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateStudentAsync(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await _context.Students.AnyAsync(s => s.StudentId == student.StudentId);
                    if (!exists)
                        return NotFound();
                }
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.DeleteStudentAsync(id);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Statistics()
        {
            ViewBag.TotalOgrenci = _context.Count();
            ViewBag.ErkekSayisi = _context.CountByGender(Gender.erkek);
            ViewBag.KadinSayisi = _context.CountByGender(Gender.kadın);
            return View();
        }


    }
}