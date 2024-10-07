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
        public async Task<IActionResult> DetailsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(M => M.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsDelete(int id, string actionType, Course course)
        {
            if (actionType == "delete")
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id, string actionType, Course Cor)
        {
            if (actionType == "Edit")
            {
                var CourseEd = await _context.Courses
                    .FirstOrDefaultAsync(m => m.CourseID == id);
                if (CourseEd == null)
                {
                    return NotFound();
                }
                return View(CourseEd);
            }

            if (actionType == "Create")
            {
                return View();
            }
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit([Bind("CourseID,Title,Credits")] Course course, string actionType)
        {
            if (actionType == "Create")
            {
                _context.Add(course);
                var CourseId = _context.Courses.OrderByDescending(m => m.CourseID).First();
                course.CourseID = course.CourseID + 1;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (actionType == "Edit")
            {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
            }
            return View(course);
        }
    }
}
