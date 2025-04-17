using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComIT_eLearning.Models.Enums;

namespace ComIT_eLearning.Models
{
  public class TeacherProfile
  {
    public int Id { get; set; }

    [Required]
    required public string UserId { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [Display(Name = "Employee Number")]
    required public string EmployeeNumber { get; set; }

    required public PositionType Position { get; set; }
    public string? Department { get; set; }
    public string? OfficeLocation { get; set; }

    public string? Description { get; set; }
    public string? WebsiteUrl { get; set; }

    public ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
  }
}