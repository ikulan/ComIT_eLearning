using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ComIT_eLearning.Models
{
  public class ApplicationUser : IdentityUser
  {
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public string PreferredName { get; set; } = String.Empty;

    // [Required]
    // public required string StudentNumber { get; set; }

    // Registration related
    public string? InvitationToken { get; set; }
    public bool IsInvitationUsed { get; set; }
    public DateTime? InvitationExpiry { get; set; }
    public string? InvitationRole { get; set; }

    public string GetDisplayName()
    {
      return string.IsNullOrEmpty(PreferredName) ? FirstName : PreferredName;
    }

    public string GetFullName()
    {
      return FirstName + " " + LastName;
    }

    public string GetAvatar()
    {
      return FirstName[0].ToString().ToUpper() + LastName[0].ToString().ToUpper();
    }

  }
}
