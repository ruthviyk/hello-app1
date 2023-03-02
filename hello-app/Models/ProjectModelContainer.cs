using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace hello_app.Models
{
    public class ProjectModelContainer: DbContext
    {
        public ProjectModelContainer() { }
        public DbSet<Project> ProjectSet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
