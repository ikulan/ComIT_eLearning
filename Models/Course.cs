using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ComIT_eLearning.Models.Enums;

namespace ComIT_eLearning.Models;

public class ClassTime
{
  [Required]
  public DayOfWeek Day { get; set; }

  [Required]
  [DataType(DataType.Time)]
  public TimeSpan StartTime { get; set; }

  [Required]
  [DataType(DataType.Time)]
  public TimeSpan EndTime { get; set; }
}

public class Course
{
  public int Id { get; set; }

  [Required]
  public CourseStatus Status { get; set; }


  [Required]
  [MaxLength(100)]
  public string Name { get; set; }

  [Required]
  [MaxLength(20)]
  public string Number { get; set; } // Course code or number

  [MaxLength(100)]
  public string Department { get; set; }

  [Required]
  public int Year { get; set; } // Academic year

  [Required]
  public SemesterType Semester { get; set; }

  [MaxLength(500)]
  public string Description { get; set; }

  [DataType(DataType.Date)]
  public DateTime StartDate { get; set; }

  [DataType(DataType.Date)]
  public DateTime EndDate { get; set; }

  // Store class times as a JSON string in the database
  private string ClassTimesJson { get; set; }

  public List<ClassTime> ClassTimes
  {
    get
    {
      if (string.IsNullOrEmpty(ClassTimesJson))
        return new List<ClassTime>();

      return JsonSerializer.Deserialize<List<ClassTime>>(ClassTimesJson) ?? new List<ClassTime>();
    }
    set => ClassTimesJson = JsonSerializer.Serialize(value);
  }

  [Url]
  public string ImageUrl { get; set; }

}
