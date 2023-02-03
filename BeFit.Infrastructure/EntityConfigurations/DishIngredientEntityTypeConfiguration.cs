namespace BeFit.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;

class DishIngredientEntityTypeConfiguration : IEntityTypeConfiguration<DishIngredient>
{
    public void Configure(EntityTypeBuilder<DishIngredient> config)
    {
        config.ToTable("DishIngredients", BeFitDbContext.DEFAULT_SCHEMA);

        config.HasKey(o => o.Id);

        config.HasOne(e => e.Dish)
            .WithMany(e => e.DishIngredients)
            .IsRequired();

        config.HasOne(e => e.Ingredient)
            .WithMany(e => e.DishIngredients)
            .IsRequired();

    }
}