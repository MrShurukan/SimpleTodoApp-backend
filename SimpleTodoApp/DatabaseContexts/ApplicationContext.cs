using Microsoft.EntityFrameworkCore;
using SimpleTodoApp.Models;

namespace SimpleTodoApp.DatabaseContexts;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}