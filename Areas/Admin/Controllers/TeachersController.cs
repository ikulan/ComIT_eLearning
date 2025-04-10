using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComIT_eLearning.Data;
using ComIT_eLearning.Models;
using ComIT_eLearning.Models.Enums;
using ComIT_eLearning.Areas.Admin.ViewModels;

namespace ComIT_eLearning.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = "Admin")]
  public class TeachersController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public TeachersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _context = context;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
      return View(await _context.TeacherProfiles.Include(tp => tp.User).ToListAsync());
    }

    public IActionResult Create()
    {
      var positionList = Enum.GetValues(typeof(PositionType))
                                 .Cast<PositionType>()
                                 .Select(s => new { Value = s, Text = s.ToString() });

      ViewBag.PositionList = new SelectList(positionList, "Value", "Text");
      return View(new CreateTeacherViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTeacherViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      // 1. Create user
      var user = new ApplicationUser
      {
        UserName = model.Email,
        Email = model.Email,
        FirstName = model.FirstName,
        LastName = model.LastName,
        PreferredName = model.PreferredName,
        PhoneNumber = model.PhoneNumber,
        EmailConfirmed = false // User must register with invitation
      };

      var result = await _userManager.CreateAsync(user);
      if (!result.Succeeded)
      {
        foreach (var error in result.Errors)
          ModelState.AddModelError(string.Empty, error.Description);
        return View(model);
      }

      // 2. Assign "Teacher" role
      if (!await _roleManager.RoleExistsAsync("Teacher"))
        await _roleManager.CreateAsync(new IdentityRole("Teacher"));

      await _userManager.AddToRoleAsync(user, "Teacher");

      // 3. Create TeacherProfile
      var profile = new TeacherProfile
      {
        UserId = user.Id,
        EmployeeNumber = model.EmployeeNumber,
        Position = model.Position,
        Department = model.Department,
        OfficeLocation = model.OfficeLocation,
        Description = model.Description,
        WebsiteUrl = model.WebsiteUrl
      };
      _context.TeacherProfiles.Add(profile);
      await _context.SaveChangesAsync();

      // 4. Generate invitation token
      var token = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, "Invitation");

      // 5. Generate registration link 
      var registrationUrl = Url.Action(
        action: "Registration",
        controller: "Account",
        values: new { area = "Identity", userId = user.Id, token },
        protocol: Request.Scheme
      );

      // TODO: send email OR show link
      TempData["SuccessMessage"] = $"Invitation link: {registrationUrl}";

      return RedirectToAction("Index", "Teachers", new { area = "Admin" });
    }
  }
}