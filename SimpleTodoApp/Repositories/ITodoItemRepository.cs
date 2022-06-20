using SimpleTodoApp.Models;

namespace SimpleTodoApp.Repositories;

public interface ITodoItemRepository : IRepository<TodoItem>
{
    void MarkTodoItem(TodoItem todoItem);
}