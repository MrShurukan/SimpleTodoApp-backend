namespace SimpleTodoApp.Contracts.Requests;

public class PutTodoItemRequest
{
    public int Id { get; set; }
    public string Text { get; set; }
}