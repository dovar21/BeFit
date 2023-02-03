namespace BeFit.Domain.SeedWork;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<T, bool>> Criteria { get; }
}