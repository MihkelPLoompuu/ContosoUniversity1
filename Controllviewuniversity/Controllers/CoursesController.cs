using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corse = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (corse == null)
            {
                return NotFound();
            }

            return View(corse);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var Corse = await _context.Courses.FirstOrDefaultAsync(M => M.CourseID == id); 

            if (Corse == null) 
            {
                return NotFound();
            }

            return View(Corse);


        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Cors = await _context.Courses.FindAsync(id); 
            _context.Courses.Remove(Cors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cors = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(m => m.CourseID == id);
            if (Cors == null)
            {
                return NotFound();
            }


            var CloneCorses = new Course
            {
                CourseID = Cors.CourseID,
                Title = Cors.Title,
                Credits = Cors.Credits,
                Enrollments = Cors.Enrollments, 
            };

            _context.Courses.Add(CloneCorses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));

        }
        [HttpGet]
        public IActionResult CreateEdit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(Course Cores) 
        {
            if (ModelState.IsValid)
            {
                _context.Add(Cores);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Cores);
        }
    }
}
