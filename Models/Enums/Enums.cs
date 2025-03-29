namespace ComIT_eLearning.Models.Enums;

public enum SemesterType
{
  Spring,
  Summer,
  Fall,
  Winter
}

public enum CourseStatus
{
  Ongoing,            // The course is currently active and running
  Upcoming,           // The course is scheduled but not yet started
  Completed,          // The course has finished
  Canceled,           // The course has been canceled
  Pending,            // The course is planned but not yet active
}
