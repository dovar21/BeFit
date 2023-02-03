namespace BeFit.Domain.SeedWork;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}
