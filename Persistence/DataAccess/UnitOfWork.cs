using System.Collections;
using System.Security.Claims;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Context;
using Utils.CurrentUser;
using static Domain.Constants.Contracts;

namespace Persistence.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly TTDContext _context;
    private Hashtable _repositories;
    private readonly UserContextBaseService _userContextBaseService;

    public UnitOfWork(TTDContext context, UserContextBaseService userContextBaseService)
    {
        _context = context;
        _userContextBaseService = userContextBaseService;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> Complete(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        CreateAudit();
        return await _context.SaveChangesAsync(cancellationToken);
    }

    private void CreateAudit()
    {
        var userId = _userContextBaseService.GetValueFromClaim(CustomClaimsNames.USER_ID_CLAIM);
        IEnumerable<EntityEntry<IActivityAuditingEntity>> entries =
            _context
                .ChangeTracker
                .Entries<IActivityAuditingEntity>()
                .ToList();
        
        foreach (var entry in entries)
        {
            var auditMessage = entry.State switch
            {
                EntityState.Deleted => CreateDeletedMessage(entry),
                EntityState.Modified => CreateModifiedMessage(entry),
                EntityState.Added => CreateAddedMessage(entry),
                _ => null
            };

          
        }

        string CreateAddedMessage(EntityEntry<IActivityAuditingEntity> entry)
            => $"Inserted {entry.Metadata.DisplayName()} Identified as {entry.Entity.GetLogIdentifier()}";

        string CreateModifiedMessage(EntityEntry<IActivityAuditingEntity> entry)
            => $"Updated {entry.Metadata.DisplayName()} Identified as {entry.Entity.GetLogIdentifier()}";

        string CreateDeletedMessage(EntityEntry<IActivityAuditingEntity> entry)
            => $"Deleted {entry.Metadata.DisplayName()} Identified as {entry.Entity.GetLogIdentifier()}";
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<BaseAuditableEntity>> entries =
            _context
                .ChangeTracker
                .Entries<BaseAuditableEntity>();        

        foreach (EntityEntry<BaseAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedAt)
                    .CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.CreatedBy)
                    .CurrentValue = _userContextBaseService.GetValueFromClaim(ClaimTypes.GivenName);
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedAt)
                    .CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.ModifiedBy)
                    .CurrentValue = _userContextBaseService.GetValueFromClaim(ClaimTypes.GivenName);
            }
        }

        IEnumerable<EntityEntry<AuditableEntity>> auditableEntries =
            _context
                .ChangeTracker
                .Entries<AuditableEntity>();

        foreach (EntityEntry<AuditableEntity> entityEntry in auditableEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedAt)
                    .CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.CreatedBy)
                    .CurrentValue = _userContextBaseService.GetValueFromClaim(ClaimTypes.GivenName);
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedAt)
                    .CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.ModifiedBy)
                    .CurrentValue = _userContextBaseService.GetValueFromClaim(ClaimTypes.GivenName);
            }
        }
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context,
                    _userContextBaseService);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }
}