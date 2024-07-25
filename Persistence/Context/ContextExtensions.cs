using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Paging;
using Domain.Specifications;

namespace Persistence.Context;

public static class ContextExtensions
{
    public static IQueryable<TEntity> WhereIf<TEntity>(
        this IQueryable<TEntity> queryable,
        Expression<Func<TEntity, bool>> predicate,
        bool condition
    )
    {
        if (condition)
        {
            return queryable.Where(predicate);
        }

        return queryable;
    }    
    public static IQueryable<TEntity> ApplyPaging<TEntity>(
        this IQueryable<TEntity> queryable,
        PagingParams pagingParams
    )
    {
        var skip = pagingParams.PageSize * (pagingParams.PageIndex - 1);
        var take = pagingParams.PageSize;
        return queryable.Skip(skip).Take(take);
    }
    
    public static IQueryable<TEntity> ApplyPaging<TEntity>(
        this IQueryable<TEntity> queryable,
        int skip,
        int take
    )
    {
        return queryable.Skip(skip).Take(take);
    }
}