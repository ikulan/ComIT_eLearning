namespace ComIT_eLearning.Areas.Admin.Models
{
  public class AddTeacherToCourseRequest
  {
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
  }
}
