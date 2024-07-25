using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);

    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    IQueryable<T> ApplySpecification(ISpecification<T> spec);

    Task<int> CountAsync(ISpecification<T> spec);

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> GetAll();
    void UpdateRange(List<T> entities);
}