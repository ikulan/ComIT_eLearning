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
      return View(user);
    }

  }
}
