namespace BeFit.Infrastructure;

using Microsoft.EntityFrameworkCore;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.SeedWork;
using BeFit.Infrastructure.EntityConfigurations;

public class BeFitDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "dbo";
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public BeFitDbContext(DbContextOptions<BeFitDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DishEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}