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
        public async Task<IActionResult> CreateEdit(int? id)
        {
            if (id == null)
            {
                ViewBag.CreateEdit = "CreateEdit";
                ViewBag.CreateEditCE = "Make a new Course";
                return View();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(M => M.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.CreateEdit = "Edit";
            ViewBag.CreateEditCE = "Edit a Course";
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEdit(Course course)
        {
                if (course.CourseID == 0)
                {
                    _context.Add(course);
                    var CourseId = _context.Courses.OrderByDescending(m => m.CourseID).First();
                    course.CourseID = CourseId.CourseID + 1;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                var COURSE = _context.Courses.AsNoTracking().FirstOrDefault(M => M.CourseID == course.CourseID);
                if (COURSE == null)
                {
                    return NotFound();
                }
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            return View(course);
        }
    }
}
