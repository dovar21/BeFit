namespace BeFit.Domain.AggregatesModel.DishOrderingAggregates; 

using BeFit.Domain.SeedWork;

public interface IDishRepository : IRepository<Dish>
{
    Dish Add(Dish dish);
    void Update(Dish dish);
    Task<Dish> FindAsync(int id);
    Task<Dish> FindAsync(ISpecification<Dish> spec);
    Task<List<Dish>> ToListAsync();

}

