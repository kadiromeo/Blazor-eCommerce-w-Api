using Microsoft.EntityFrameworkCore;

namespace Blazor_eCommerce_Project.Data.Access.Data
{
    public class CourseContext:DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options):base(options)
        {
        }

        public DbSet<Course> Cources { get; set; }

    } 
}
