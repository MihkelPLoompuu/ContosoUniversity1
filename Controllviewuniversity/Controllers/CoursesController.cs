using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> DetailsDelete(int? id, string actionType)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["actionType"] = actionType ?? "Details";
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsDeleteConfirmed(int id, string actionType, Course course)
        {
            if (actionType == "Details")
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id, string actionType)
        {
            if (id == null && actionType == "Create")
            {
                return View(new Course());
            }
            else
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    return NotFound();
                }
                return View(course);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(int? id, [Bind("CourseID,Title,Credits")] Course course, string actionType)
        {

            if (actionType == "Create")
            {
                var CourseId = await _context.Courses.MaxAsync(m => (int?)m.CourseID) ?? 0;
                course.CourseID = CourseId + 1;

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else if (actionType == "Edit" && id != null)
            {

                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }
    }
}
