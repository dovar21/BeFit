namespace BeFit.Domain.Specifications.DishOrdering;

using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.SeedWork;

public class IngredientUniquenessCheckSpecification : BaseSpecification<Ingredient>
{
    public IngredientUniquenessCheckSpecification(int? id, string name)
        : base(i => (!(id > 0) ||  i.Id != id) &&
        (!(name != null) || i.Name == name))
    {

    }
}