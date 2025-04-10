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
      return View(new TeacherViewModel());
    }

    public async Task<IActionResult> Edit(string userId)
    {
      var user = await _userManager.FindByIdAsync(userId);
      var profile = await _context.TeacherProfiles.FirstOrDefaultAsync(p => p.UserId == userId);

      if (user == null || profile == null)
      {
        return NotFound();
      }

      var model = new TeacherViewModel
      {
        UserId = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        PreferredName = user.PreferredName,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,

        EmployeeNumber = profile.EmployeeNumber,
        Position = profile.Position,
        Department = profile.Department,
        OfficeLocation = profile.OfficeLocation,
        Description = profile.Description,
        WebsiteUrl = profile.WebsiteUrl
      };

      var positionList = Enum.GetValues(typeof(PositionType))
                                 .Cast<PositionType>()
                                 .Select(s => new { Value = s, Text = s.ToString() });

      ViewBag.PositionList = new SelectList(positionList, "Value", "Text");
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TeacherViewModel model)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TeacherViewModel model)
    {
      if (!ModelState.IsValid || string.IsNullOrEmpty(model.UserId))
      {
        return View(model);
      }

      var user = await _userManager.FindByIdAsync(model.UserId);
      if (user == null) return NotFound();

      user.FirstName = model.FirstName;
      user.LastName = model.LastName;
      user.PreferredName = model.PreferredName;
      user.Email = model.Email;
      user.UserName = model.Email;
      user.PhoneNumber = model.PhoneNumber;

      await _userManager.UpdateAsync(user);

      var profile = await _context.TeacherProfiles.FirstOrDefaultAsync(p => p.UserId == model.UserId);
      if (profile != null)
      {
        profile.EmployeeNumber = model.EmployeeNumber;
        profile.Position = model.Position;
        profile.Department = model.Department;
        profile.OfficeLocation = model.OfficeLocation;
        profile.Description = model.Description;
        profile.WebsiteUrl = model.WebsiteUrl;

        _context.TeacherProfiles.Update(profile);
        await _context.SaveChangesAsync();
      }

      TempData["SuccessMessage"] = "Teacher profile updated successfully.";

      return RedirectToAction("Index", "Teachers", new { area = "Admin" });
    }

  }
}