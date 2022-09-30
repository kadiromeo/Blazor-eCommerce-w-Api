using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazor_eCommerce_Project.Data.Access.Data
{
    public class CourseContext:IdentityDbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options):base(options)
        {
        }

        public DbSet<Course> Cources { get; set; }

    } 
}
