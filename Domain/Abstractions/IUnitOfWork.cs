using Domain.Abstractions.Repositories;
using Domain.Entities.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> Complete(CancellationToken cancellationToken = default);
}