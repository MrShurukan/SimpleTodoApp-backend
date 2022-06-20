using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleTodoApp.DatabaseContexts;
using SimpleTodoApp.Models;

namespace SimpleTodoApp.Repositories;

public class CategoryDatabaseRepository : DatabaseRepository<Category>, ICategoryRepository
{
    public CategoryDatabaseRepository(ApplicationContext context) : base(context)
    {
    }

    protected override IQueryable<Category> GetQueryable(Expression<Func<Category, bool>> lambdaExpression) =>
        GetQueryable()
            .Where(lambdaExpression);

    protected override IQueryable<Category> GetQueryable() =>
        Context.Categories
            .Include(c => c.TodoItems);
}