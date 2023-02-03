namespace BeFit.Domain.Specifications.DishOrdering;

using BeFit.Domain.SeedWork;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;

public class DishUniquenessCheckSpecification : BaseSpecification<Dish>
{
    public DishUniquenessCheckSpecification(int? id, string name)
        : base(i => (!(id > 0) ||  i.Id != id) &&
        (!(name != null) || i.Name == name))
    {

    }
}