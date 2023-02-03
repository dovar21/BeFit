namespace BeFit.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.SeedWork;

public class DishRepository: IDishRepository
{
    private readonly BeFitDbContext _context;

    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public DishRepository(BeFitDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Dish Add(Dish dish)
    {
        return _context.Dishes.Add(dish).Entity;
    }

    public async Task<Dish> FindAsync(int id)
    {
        var dish = await _context
                            .Dishes.Include(e=>e.DishIngredients).ThenInclude(e=>e.Ingredient)
                            .FirstOrDefaultAsync(o => o.Id == id && o.IsEnabled);
        return dish;
    }
    //Find by specification
    public async Task<Dish> FindAsync(ISpecification<Dish> spec)
    {
        return await ApplySpecification(spec).SingleOrDefaultAsync();
    }
    //Find all by specification
    public async Task<List<Dish>> ToListAsync()
    {
        return await _context
                            .Dishes.Where(e => e.IsEnabled).AsNoTracking().ToListAsync();
    }
    public void Update(Dish dish)
    {
        _context.Entry(dish).State = EntityState.Modified;
    }
    //Apply specification
    private IQueryable<Dish> ApplySpecification(ISpecification<Dish> spec)
    {
        return SpecificationEvaluator<Dish>.GetQuery(_context.Set<Dish>().AsQueryable(), spec);
    }
}
