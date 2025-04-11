using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ComIT_eLearning.Models;
using ComIT_eLearning.Attributes;

namespace ComIT_eLearning.Controllers;

public class DashboardController : Controller
{
  private readonly ILogger<DashboardController> _logger;

  public DashboardController(ILogger<DashboardController> logger)
  {
    _logger = logger;
  }

  [WIP]
  public IActionResult Index()
  {
    return View();
  }

  [WIP]
  public IActionResult Help()
  {
    return View();
  }
}