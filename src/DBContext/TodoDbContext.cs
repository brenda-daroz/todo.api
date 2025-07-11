using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPI.Models;

namespace TodoAPI.AppDataContext
{

    // TodoDbContext class inherits from DbContext
    public class TodoDbContext : DbContext
    {

        // DbSettings field to store the connection string
        private readonly DbSettings _dbsettings;

        // Constructor to inject the DbSettings model
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        // DbSet property to represent the Todo table
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todos");
        }
    }
}