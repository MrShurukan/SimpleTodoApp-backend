using System.ComponentModel.DataAnnotations;

namespace SimpleTodoApp.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<TodoItem> TodoItems { get; set; }
}