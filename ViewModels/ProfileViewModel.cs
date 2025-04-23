using System.ComponentModel.DataAnnotations;

namespace ComIT_eLearning.Models;

public class ProfileViewModel
{
  public ApplicationUser User { get; set; }
  public StudentProfile? studentProfile { get; set; } = null;
  public TeacherProfile? teacherProfile { get; set; } = null;


}