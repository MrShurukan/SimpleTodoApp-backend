using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleTodoApp.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category Category { get; set; }
    
    public string Text { get; set; }
    public bool IsChecked { get; set; }
}