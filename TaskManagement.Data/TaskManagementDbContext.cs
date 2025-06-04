using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Tasks.Models;
using TaskManagement.Domain.Users.Models;

namespace TaskManagement.Data
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
