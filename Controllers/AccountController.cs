using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ComIT_eLearning.Models;

namespace ComIT_eLearning.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager; // for a user
    private readonly IUserStore<ApplicationUser> _userStore;    // for the user database
    //private readonly IUserEmailStore<ApplicationUser> _emailStore;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
      SignInManager<ApplicationUser> signInManager,
      UserManager<ApplicationUser> userManager,
      IUserStore<ApplicationUser> userStore,
      ILogger<AccountController> logger)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _userStore = userStore;
      _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
      var user = await _userManager.GetUserAsync(User);
      if (user == null)
      {
        return NotFound("User not found.");
      }

      var viewModel = new AccountViewModel();
      viewModel.User = user;
      return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(AccountViewModel model)
    {
      // skip validation on User field
      ModelState.Remove(nameof(model.User));

      if (!ModelState.IsValid)
      {
        return RedirectToAction("Index");
      }

      var user = await _userManager.GetUserAsync(User);
      if (user == null)
      {
        return RedirectToAction("Login", "Account");
      }

      var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
      if (result.Succeeded)
      {
        await _signInManager.RefreshSignInAsync(user);
        TempData["SuccessMessage"] = "Password changed successfully.";
        return RedirectToAction("Index");
      }

      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }

      model.User = user;
      return View("Index", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePreferredName(AccountViewModel model)
    {
      var user = await _userManager.GetUserAsync(User);
      if (user == null)
      {
        return RedirectToAction("Login", "Account");
      }

      if (string.IsNullOrWhiteSpace(model.User.PreferredName) && string.IsNullOrWhiteSpace(user.PreferredName))
      {
        return RedirectToAction("Index");
      }

      var rawName = model.User.PreferredName?.Trim();
      if (!string.IsNullOrWhiteSpace(rawName))
      {
        user.PreferredName = ToTitleCase(rawName);
      }
      else
      {
        user.PreferredName = String.Empty;
      }


      var result = await _userManager.UpdateAsync(user);
      if (result.Succeeded)
      {
        TempData["SuccessMessage"] = "Preferred name updated successfully.";
      }
      else
      {
        TempData["ErrorMessages"] = string.Join(" | ", result.Errors.Select(e => e.Description));
      }

      return RedirectToAction("Index");
    }

    private string ToTitleCase(string input)
    {
      return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
    }

  }
}
