namespace SimpleTodoApp.Contracts.Requests;

public class PutCategoryRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}