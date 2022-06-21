using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleTodoApp.DatabaseContexts;

namespace SimpleTodoApp.Repositories;

public abstract class DatabaseRepository<TItem> : IRepository<TItem>
    where TItem : class
{
    protected readonly ApplicationContext Context;
    protected readonly DbSet<TItem> DbSet;

    public DatabaseRepository(ApplicationContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = typeof(ApplicationContext)
            .GetProperties()
            .FirstOrDefault(x => x.PropertyType == typeof(DbSet<TItem>))?
            .GetValue(Context) as DbSet<TItem> 
                ?? throw new InvalidDataException("В ApplicationContext не было нужного типа DbSet");
    }

    public void AddRange(IEnumerable<TItem> item)
    {
        DbSet.AddRange(item);
        Context.SaveChanges();
    }

    public void Add(TItem item)
    {
        DbSet.Add(item);
        Context.SaveChanges();
    }

    protected abstract IQueryable<TItem> GetQueryable(Expression<Func<TItem, bool>> lambdaExpression);
    protected abstract IQueryable<TItem> GetQueryable();

    public TItem? Get(Expression<Func<TItem, bool>> lambdaExpression) =>
        GetQueryable(lambdaExpression).FirstOrDefault();

    public TItem? Get()
        => GetQueryable().FirstOrDefault();

    public List<TItem> GetList(Expression<Func<TItem, bool>> lambdaExpression) =>
        GetQueryable(lambdaExpression).ToList();

    public List<TItem> GetList() =>
        GetQueryable().ToList();

    public void Remove(TItem item)
    {
        DbSet.Remove(item);
        Context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<TItem> items)
    {
        DbSet.RemoveRange(items);
        Context.SaveChanges();
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }
}