
using EfCoreApplication.Db;
using EfCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            _context.Students.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var ogr= await _context.Ogrenciler.FindAsync(id);
            var ogr = await _context.Students.FirstOrDefaultAsync(o => o.StudentId == id);

            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Students.Update(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(o => o.StudentId == student.StudentId))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.StudentId == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Statistics()
        {
            var total = _context.Students.Count();
            var man = _context.Students.Count(x => x.Gender == Gender.erkek);
            var woman = _context.Students.Count(x => x.Gender == Gender.kadın);

            //var bolumler = _context.Ogrenci
            //    .GroupBy(x => x.Bolum)
            //    .Select(g => new { Bolum = g.Key, Sayisi = g.Count() })
            //    .ToList();

            ViewBag.TotalOgrenci = total;
            ViewBag.ErkekSayisi = man;
            ViewBag.KadinSayisi = woman;
            //ViewBag.Bolumler = bolumler.Select(x => x.Bolum).ToList();
            //ViewBag.BolumSayilari = bolumler.Select(x => x.Sayisi).ToList();

            return View();
        }


    }
}