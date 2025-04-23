using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ComIT_eLearning.Data;
using ComIT_eLearning.Models;
using ComIT_eLearning.Attributes;

namespace ComIT_eLearning.Controllers;

[Authorize]
public class DashboardController : Controller
{
  private readonly ILogger<DashboardController> _logger;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;
  private readonly ApplicationDbContext _context;


  public DashboardController(ILogger<DashboardController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
  {
    _logger = logger;
    _userManager = userManager;
    _roleManager = roleManager;
    _context = context;
  }

  public async Task<IActionResult> Index()
  {
    ApplicationUser currentUser = await _userManager.GetUserAsync(User);
    List<Course> courses = new();


    if (User.IsInRole("Student"))
    {
      var profile = await _context.StudentProfiles
          .Include(p => p.EnrolledCourses)
          .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);
      courses = profile?.EnrolledCourses.ToList() ?? new();
    }
    else if (User.IsInRole("Teacher"))
    {
      var profile = await _context.TeacherProfiles
          .Include(p => p.EnrolledCourses)
          .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);
      courses = profile?.EnrolledCourses.ToList() ?? new();
    }

    return View(courses);
  }

  [WIP]
  public IActionResult Assignment()
  {
    return View();
  }

  [WIP]
  public IActionResult Calendar()
  {
    return View();
  }

  [WIP]
  public IActionResult Help()
  {
    return View();
  }
}