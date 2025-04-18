namespace ComIT_eLearning.Models
{
  public class AddStudentsToCourseRequest
  {
    public int CourseId { get; set; }
    public List<int> StudentIds { get; set; } = new();
  }
}