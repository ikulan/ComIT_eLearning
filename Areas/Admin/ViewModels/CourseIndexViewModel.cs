using ComIT_eLearning.Models;
using ComIT_eLearning.Models.Enums;


namespace ComIT_eLearning.Areas.Admin.Models
{
  public class CourseIndexViewModel
  {
    public List<Course> CourseList { get; set; }
    public string EmptyMessage { get; set; }

    public string SearchValue { get; set; } = "";
    public CourseStatus? SelectedStatus { get; set; } = null;

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public int TotalCount { get; set; } = 0;
  }
}
