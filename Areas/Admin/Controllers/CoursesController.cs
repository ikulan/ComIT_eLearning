using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComIT_eLearning.Data;
using ComIT_eLearning.Models;
using ComIT_eLearning.Models.Enums;

namespace ComIT_eLearning.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            var semesterList = Enum.GetValues(typeof(SemesterType))
                                 .Cast<SemesterType>()
                                 .Select(s => new { Value = s, Text = s.ToString() });
            var statusList = Enum.GetValues(typeof(CourseStatus))
                     .Cast<CourseStatus>()
                     .Select(s => new { Value = s, Text = s.ToString() });
            ViewBag.SemesterList = new SelectList(semesterList, "Value", "Text");
            ViewBag.StatusList = new SelectList(statusList, "Value", "Text", CourseStatus.Pending);

            return View(new Course());
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,Name,Number,Department,Year,Semester,Description,StartDate,EndDate,ImageUrl")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var semesterList = Enum.GetValues(typeof(SemesterType))
                     .Cast<SemesterType>()
                     .Select(s => new { Value = s, Text = s.ToString() });
            var statusList = Enum.GetValues(typeof(CourseStatus))
                     .Cast<CourseStatus>()
                     .Select(s => new { Value = s, Text = s.ToString() });
            ViewBag.SemesterList = new SelectList(semesterList, "Value", "Text");
            ViewBag.StatusList = new SelectList(statusList, "Value", "Text", CourseStatus.Pending);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Name,Number,Department,Year,Semester,Description,StartDate,EndDate,ImageUrl")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListPartial(string? searchString, int page = 1, int pageSize = 10)
        {
            var query = _context.Courses.Where(c => c.Status == CourseStatus.Active);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var lowerSearch = searchString.ToLower();
                query = query.Where(c =>
                    c.Name.ToLower().Contains(lowerSearch) ||
                    c.Number.ToLower() == lowerSearch);
            }

            var totalCount = await query.CountAsync(); // Optional: For showing total pages

            var pagedCourses = await query
                .OrderByDescending(c => c.StartDate)
                .ThenBy(c => c.Number)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return PartialView("_CourseListPartial", new CourseListViewModel
            {
                CourseList = pagedCourses,
                EmptyMessage = "No courses found matching your search.",
                showStatus = false,
                showAddButton = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudents([FromBody] AddStudentsToCourseRequest request)
        {
            var course = await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == request.CourseId);

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            var students = await _context.StudentProfiles
                .Where(s => request.StudentIds.Contains(s.Id))
                .ToListAsync();

            foreach (var student in students)
            {
                if (!course.Students.Any(s => s.Id == student.Id))
                {
                    course.Students.Add(student);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = true, added = students.Count });
        }

        public async Task<IActionResult> RemoveStudent(int courseId, int studentId, string? returnUrl = null)
        {
            var course = await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
                return NotFound("Course not found.");

            var student = await _context.StudentProfiles.FindAsync(studentId);
            if (student == null)
                return NotFound("Student not found.");

            if (course.Students.Any(s => s.Id == studentId))
            {
                course.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            returnUrl ??= Url.Action("Details", new { id = courseId });

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacher([FromBody] AddTeacherToCourseRequest request)
        {
            var course = await _context.Courses
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(c => c.Id == request.CourseId);

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            var teacher = await _context.TeacherProfiles.FindAsync(request.TeacherId);
            if (teacher == null)
            {
                return NotFound("Teacher not found.");
            }

            if (!course.Teachers.Any(s => s.Id == teacher.Id))
            {
                course.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
            }

            return Ok(new { success = true });
        }

        public async Task<IActionResult> RemoveTeacher(int courseId, int teacherId, string? returnUrl = null)
        {
            var course = await _context.Courses
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
                return NotFound("Course not found.");

            var teacher = await _context.TeacherProfiles.FindAsync(teacherId);
            if (teacher == null)
                return NotFound("Teacher not found.");

            if (course.Teachers.Any(s => s.Id == teacherId))
            {
                course.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }

            returnUrl ??= Url.Action("Details", new { id = courseId });

            return LocalRedirect(returnUrl);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
