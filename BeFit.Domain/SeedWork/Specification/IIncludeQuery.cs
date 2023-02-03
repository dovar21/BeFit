namespace BeFit.Domain.SeedWork;

using System.Collections.Generic;

public interface IIncludeQuery
{
    Dictionary<IIncludeQuery, string> PathMap { get; }
    IncludeVisitor Visitor { get; }
    HashSet<string> Paths { get; }
}

public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
{

}