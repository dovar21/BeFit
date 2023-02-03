
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.SeedWork;

public interface IIngredientRepository : IRepository<Ingredient>
{
    Ingredient Add(Ingredient ingredient);
    void Update(Ingredient ingredient);
    Task<Ingredient> FindAsync(int id);
    Task<Ingredient> FindAsync(ISpecification<Ingredient> spec);
    Task<List<Ingredient>> ToListAsync();

}

