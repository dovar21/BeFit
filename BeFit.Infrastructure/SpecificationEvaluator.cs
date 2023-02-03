namespace BeFit.Infrastructure;

using Microsoft.EntityFrameworkCore;
using BeFit.Domain.SeedWork;
using System.Linq;

public class SpecificationEvaluator<T> where T : Entity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        // modify the IQueryable using the specification's criteria expression
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        return query;
    }
}