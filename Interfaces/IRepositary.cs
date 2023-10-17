using System.Linq.Expressions;

namespace Todo.Service.Interfaces;

public interface IRepository<T> where T : IEntity
{
    void Create(T item);
    T Get(Guid id);
    T Get(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
    void Remove(Guid id);
    void RemoveAll(Expression<Func<T, bool>> filter);
    void Update(T item);
}
