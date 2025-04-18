namespace ComIT_eLearning.Models
{
  public class CourseListViewModel
  {
    public List<Course> CourseList { get; set; }
    public string EmptyMessage { get; set; }

    public bool showStatus { get; set; } = true;
    public bool showAddButton { get; set; } = false;
    public bool showRemoveButton { get; set; } = false;
  }
}
