namespace BeFit.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.SeedWork;

public class IngredientRepository: IIngredientRepository
{
    private readonly BeFitDbContext _context;

    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public IngredientRepository(BeFitDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Ingredient Add(Ingredient ingredient)
    {
        return _context.Ingredients.Add(ingredient).Entity;
    }

    public async Task<Ingredient> FindAsync(int id)
    {
        return await _context.Ingredients.FirstOrDefaultAsync(o => o.Id == id && o.IsEnabled);
    }
    //Find by specification
    public async Task<Ingredient> FindAsync(ISpecification<Ingredient> spec)
    {
        return await ApplySpecification(spec).SingleOrDefaultAsync();
    }
    //Find all
    public async Task<List<Ingredient>> ToListAsync()
    {
        return await _context.Ingredients.Where(e=>e.IsEnabled).AsNoTracking().ToListAsync();
    }
    public void Update(Ingredient ingredient)
    {
        _context.Entry(ingredient).State = EntityState.Modified;
    }
    //Apply specification
    private IQueryable<Ingredient> ApplySpecification(ISpecification<Ingredient> spec)
    {
        return SpecificationEvaluator<Ingredient>.GetQuery(_context.Set<Ingredient>().AsQueryable(), spec);
    }
}
