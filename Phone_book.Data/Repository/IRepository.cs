using System.Collections.Generic;

namespace Phone_book.Data.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> All { get; }
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    TEntity FindById(int id);
}