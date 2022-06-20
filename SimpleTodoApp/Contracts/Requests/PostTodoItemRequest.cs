namespace SimpleTodoApp.Contracts.Requests;

public class PostTodoItemRequest
{
    public int CategoryId { get; set; }
    
    public string Text { get; set; } = null!;
}