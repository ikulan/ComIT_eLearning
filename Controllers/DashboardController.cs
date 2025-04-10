using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ComIT_eLearning.Models;

namespace ComIT_eLearning.Controllers;

public class DashboardController : Controller
{
  private readonly ILogger<DashboardController> _logger;

  public DashboardController(ILogger<DashboardController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    return View();
  }
}