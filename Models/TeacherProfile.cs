using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComIT_eLearning.Models.Enums;

namespace ComIT_eLearning.Models
{
  public class TeacherProfile
  {
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    public int TeacherId { get; set; }
    public PositionType Position { get; set; }
    public string? Department { get; set; }
    public string? OfficeLocation { get; set; }

    public string? Description { get; set; }
    public string? WebsiteUrl { get; set; }

  }
}