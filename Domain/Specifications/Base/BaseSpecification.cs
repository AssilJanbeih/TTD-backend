using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Abstractions;

namespace Domain.Specifications.Base;

public class BaseSpecification<T> : ISpecification<T>
{
    /// <summary>
    /// if i need it for only including entities 
    /// </summary>
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Default empty list
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } =
        new List<Expression<Func<T, object>>>();

    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    /// <summary>
    /// protected so only the child classes can benefit from it
    /// Method to fill the Include list
    /// </summary>
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    /// <summary>
    /// Fill the order by
    /// </summary>
    /// <param name="orderByExpression"></param>
    protected void AddOrderBy(Expression<Func<T, Object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    /// <summary>
    /// Fill the order by desc
    /// </summary>
    /// <param name="orderByDescExpression"></param>
    protected void AddOrderByDescending(Expression<Func<T, Object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}