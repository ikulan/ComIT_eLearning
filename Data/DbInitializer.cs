using Microsoft.AspNetCore.Identity;
using ComIT_eLearning.Models;
using ComIT_eLearning.Models.Enums;

namespace ComIT_eLearning.Data;

public static class DbInitializer
{
  public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
  {
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var roles = new[] { "Admin", "Teacher", "Student" };

    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole(role));
      }
    }

    string adminEmail = "ikulan12.admin@gmail.com";
    string adminPassword = "P@ssw0rd123";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
      var user = new ApplicationUser
      {
        FirstName = "Patty",
        LastName = "Liu",
        UserName = adminEmail,
        Email = adminEmail,
        EmailConfirmed = true,
        IsActive = true
      };

      var result = await userManager.CreateAsync(user, adminPassword);
      if (result.Succeeded)
      {
        await userManager.AddToRoleAsync(user, "Admin");
      }
    }
  }

  public static async Task SeedCourses(IServiceProvider serviceProvider)
  {
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Courses.Any())
    {
      var courses = new List<Course>
      {
        new Course
        {
          Status = CourseStatus.Pending,
          Name = "Intro to Web Development",
          Number = "CS 201",
          Department = "Computer Science",
          Year = 2025,
          Semester = SemesterType.Fall,
          Description = "A beginner course covering HTML, CSS, and JavaScript.",
          StartDate = new DateTime(2025, 9, 1),
          EndDate = new DateTime(2025, 12, 15),
          ClassTimes = new List<ClassTime>
          {
              new ClassTime { Day = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(12, 00, 0) },
              new ClassTime { Day = DayOfWeek.Wednesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(12, 300, 0) }
          },
          ImageUrl = "https://placehold.co/600x400"
        },
        new Course
        {
          Status = CourseStatus.Active,
          Name = "Scientography",
          Number = "SS 112",
          Department = "Social Science",
          Year = 2025,
          Semester = SemesterType.Spring,
          Description = "A multi-disciplinary field that uses a range of technology to answer the biggest quests of today, like: \"Why is that blue?\", \"Does it melt?\" and \"How long is a piece of string?\"",
          StartDate = new DateTime(2025, 1, 15),
          EndDate = new DateTime(2025, 5, 1),
          ClassTimes = new List<ClassTime>
          {
              new ClassTime { Day = DayOfWeek.Tuesday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 30, 0) },
              new ClassTime { Day = DayOfWeek.Thursday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 30, 0) }
          },
          ImageUrl = "https://placehold.co/600x400"
        },
        new Course
        {
            Status = CourseStatus.Completed,
            Name = "Virtual Normality",
            Number = "SS 201",
            Department = "Social Science",
            Year = 2025,
            Semester = SemesterType.Fall,
            Description = "A new era of technology is here, and it's sure to redefine the way you think about goggles. Students can experience being where they're not, like never before. Discover new worlds, or practice tying your shoes, all in the comfort, and privacy, of the virtual realm.",
            StartDate = new DateTime(2024, 9, 15),
            EndDate = new DateTime(2024, 12, 20),
            ClassTimes = new List<ClassTime>
            {
                new ClassTime { Day = DayOfWeek.Tuesday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0) },
                new ClassTime { Day = DayOfWeek.Thursday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0) }
            },
            ImageUrl = "https://placehold.co/600x400"
        },
        new Course
        {
            Status = CourseStatus.Cancelled,
            Name = "Gastronomy",
            Number = "CL 201",
            Department = "Culinary",
            Year = 2025,
            Semester = SemesterType.Winter,
            Description = "An intensive education in Two Point County cuisine. Learn countless classic techniques, the delicate art of seasoning and how to correctly peel an onion.",
            StartDate = new DateTime(2025, 1, 6),
            EndDate = new DateTime(2024, 3, 20),
            ClassTimes = new List<ClassTime>
            {
                new ClassTime { Day = DayOfWeek.Tuesday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0) },
                new ClassTime { Day = DayOfWeek.Thursday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0) }
            },
            ImageUrl = "https://placehold.co/600x400"
        }
    };

      context.Courses.AddRange(courses);
      context.SaveChanges();
    }
  }
}
