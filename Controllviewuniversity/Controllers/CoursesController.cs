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
            if (actionType == "Clone")
            {
                if (id == null)
                {
                    return NotFound();
                }
                var CourseED = await _context.Courses
                    .FirstOrDefaultAsync(M => M.CourseID == id);
                if (CourseED == null)
                {
                    return NotFound();
                }
                return View(CourseED);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id, string actionType, Course Cor)
        {
            if (actionType != "Create")
            {
                if (id == null)
                {
                    return NotFound();
                }
                var CourseED = await _context.Courses
                    .FirstOrDefaultAsync(m => m.CourseID == id);
                if (CourseED == null)
                {
                    return NotFound();
                }
                return View(CourseED);
            }

            if (actionType == "Create")
            {
                return View();
            }
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(int? id, [Bind("CourseID,Title,Credits")] Course course, string actionType)
        {
            if (actionType == "Create")
            {
                _context.Add(course);
                var CourseId = _context.Courses.OrderByDescending(m => m.CourseID).First();
                course.CourseID = CourseId.CourseID + 1;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (actionType == "Edit")
            {
                    if (course.CourseID == null)
                    {
                        return BadRequest();
                    }
                    _context.Courses.Update(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
            }
            return View(course);
        }
    }
}
