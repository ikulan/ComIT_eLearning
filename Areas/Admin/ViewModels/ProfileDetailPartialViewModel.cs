using ComIT_eLearning.Models;

namespace ComIT_eLearning.Areas.Admin.Models
{
  public class ProfileDetailPartialViewModel
  {
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string IdNumber { get; set; } // StudentNumber or EmployeeNumber
    public ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
  }
}