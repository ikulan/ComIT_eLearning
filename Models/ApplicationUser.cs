using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ComIT_eLearning.Models
{
  public class ApplicationUser : IdentityUser
  {
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public string? PreferredName { get; set; } = String.Empty;

    // Registration related
    public bool IsActive { get; set; }
    public string? InvitationToken { get; set; }
    public DateTime? InvitationExpiry { get; set; }
    [NotMapped]
    public string AccountStatus => GetStatus();

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

    public string GetStatus()
    {
      if (!string.IsNullOrEmpty(InvitationToken))
        return "Invited";

      if (InvitationExpiry.HasValue && InvitationExpiry < DateTime.UtcNow)
        return "Expired";

      if (IsActive)
        return "Active";

      return "Inactive";
    }

  }
}
