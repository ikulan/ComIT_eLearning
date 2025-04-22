using ComIT_eLearning.Models;

namespace ComIT_eLearning.Areas.Admin.Models
{
  public class CourseListViewModel
  {
    public List<Course> CourseList { get; set; }
    public string EmptyMessage { get; set; }

    public bool ShowStatus { get; set; } = true;
    public bool ShowDetailLink { get; set; } = false;
    public bool ShowAddButton { get; set; } = false;
    public bool ShowRemoveButton { get; set; } = false;

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public int TotalCount { get; set; }
  }
}
