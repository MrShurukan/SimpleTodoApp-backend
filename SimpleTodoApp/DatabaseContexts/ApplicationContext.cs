using Microsoft.EntityFrameworkCore;
using SimpleTodoApp.Models;

namespace SimpleTodoApp.DatabaseContexts;

public class ApplicationContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
}