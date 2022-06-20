using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTodoApp.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public string Text { get; set; }
    public bool IsChecked { get; set; }
}