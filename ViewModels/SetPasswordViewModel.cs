using System.ComponentModel.DataAnnotations;

namespace ComIT_eLearning.Models;

public class SetPasswordViewModel
{
  public string? UserId { get; set; }
  public string? Token { get; set; }

  [Required]
  [DataType(DataType.Password)]
  public string? NewPassword { get; set; }

  [DataType(DataType.Password)]
  [Compare("NewPassword")]
  public string? ConfirmPassword { get; set; }
}

