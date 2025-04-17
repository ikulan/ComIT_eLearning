using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ComIT_eLearning.Models;

namespace ComIT_eLearning.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<TeacherProfile> TeacherProfiles { get; set; }
    public DbSet<StudentProfile> StudentProfiles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<StudentProfile>()
          .HasMany(s => s.EnrolledCourses)
          .WithMany(c => c.Students)
          .UsingEntity(j => j.ToTable("StudentCourses"));

      modelBuilder.Entity<TeacherProfile>()
          .HasMany(s => s.EnrolledCourses)
          .WithMany(c => c.Teachers)
          .UsingEntity(j => j.ToTable("TeacherCourses"));
    }
  }
}