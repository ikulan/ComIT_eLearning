using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
  // Registration related
  public string? InvitationToken { get; set; }
  public bool IsInvitationUsed { get; set; }
  public DateTime? InvitationExpiry { get; set; }
  public string? InvitationRole { get; set; }
}
