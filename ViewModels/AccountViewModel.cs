using System.ComponentModel.DataAnnotations;

namespace ComIT_eLearning.Models;

public class AccountViewModel
{
  public ApplicationUser User { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Current Password")]
  public string OldPassword { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "New Password")]
  public string NewPassword { get; set; }

  [DataType(DataType.Password)]
  [Display(Name = "Confirm New Password")]
  [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
  public string ConfirmPassword { get; set; }
}
