using System.Linq.Expressions;

namespace SimpleTodoApp.Repositories;

public interface IRepository<T>
{
    void AddRange(IEnumerable<T> item);
    void Add(T item);
    T? Get(Expression<Func<T, bool>> lambdaExpression);
    T? Get();
    List<T> GetList(Expression<Func<T, bool>> lambdaExpression);
    List<T> GetList();
    void Remove(T item);
    void RemoveRange(IEnumerable<T> items);
    void SaveChanges();
}