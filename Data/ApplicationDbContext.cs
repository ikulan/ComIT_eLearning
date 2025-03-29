using Microsoft.EntityFrameworkCore;
using ComIT_eLearning.Models;

namespace ComIT_eLearning.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
  }
}
