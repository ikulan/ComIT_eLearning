using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComIT_eLearning.Models
{

  public class StudentProfile
  {
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [Display(Name = "Student Number")]
    public string StudentNumber { get; set; }
    public string? Department { get; set; }
  }

}