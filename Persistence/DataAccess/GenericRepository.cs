using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Constants;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Utils.CurrentUser;

namespace Persistence.DataAccess;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TTDContext _context;
    private readonly UserContextBaseService _userContextBaseService;

    public GenericRepository(TTDContext context, UserContextBaseService userContextBaseService)
    {
        _context = context;
        _userContextBaseService = userContextBaseService;
    }

    public void Add(T entity)
    {
        if (typeof(T).IsSubclassOf(typeof(BaseAuditableEntity)))
        {
            var userId = _userContextBaseService.GetValueFromClaim(Contracts.CustomClaimsNames.USER_ID_CLAIM);
            BaseAuditableEntity auditableEntity = entity as BaseAuditableEntity;
            auditableEntity.CreatedBy = userId;
        }
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        if (typeof(T).IsSubclassOf(typeof(BaseAuditableEntity)))
        {
            var userId = _userContextBaseService.GetValueFromClaim(Contracts.CustomClaimsNames.USER_ID_CLAIM);
            BaseAuditableEntity auditableEntity = entity as BaseAuditableEntity;
            auditableEntity.ModifiedBy = userId;
            auditableEntity.ModifiedAt = DateTime.UtcNow;
        }
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsQueryable();
    }

    public void UpdateRange(List<T> entities)
    {
        _context
            .Set<T>()
            .UpdateRange(entities);
    }
}