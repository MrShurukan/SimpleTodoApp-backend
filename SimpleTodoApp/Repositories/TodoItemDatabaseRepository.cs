using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleTodoApp.DatabaseContexts;
using SimpleTodoApp.Models;

namespace SimpleTodoApp.Repositories;

public class TodoItemDatabaseRepository : DatabaseRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemDatabaseRepository(ApplicationContext context) : base(context)
    {
    }

    protected override IQueryable<TodoItem> GetQueryable(Expression<Func<TodoItem, bool>> lambdaExpression) =>
        GetQueryable()
            .Where(lambdaExpression);

    protected override IQueryable<TodoItem> GetQueryable() =>
        Context.TodoItems
            .Include(todo => todo.Category);

    public void MarkTodoItem(TodoItem todoItem)
    {
        todoItem.IsChecked = !todoItem.IsChecked;
        Context.SaveChanges();
    }
}