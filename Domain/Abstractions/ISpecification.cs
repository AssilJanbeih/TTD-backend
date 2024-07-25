using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Abstractions;

public interface ISpecification<T>
{
    /// <summary>
    /// For passing filters for the generic repo (queries)
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }
    
    /// <summary>
    /// For include statements (list of includes)
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }
    
    /// <summary>
    /// Ordering
    /// </summary>
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    
    /// <summary>
    /// Pagination
    /// </summary>
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}