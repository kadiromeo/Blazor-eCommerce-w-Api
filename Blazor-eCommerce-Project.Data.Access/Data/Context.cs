using Microsoft.EntityFrameworkCore;

namespace Blazor_eCommerce_Project.Data.Access.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
        }

        public DbSet<Cource> Cources { get; set; }

    } 
}
