namespace BeFit.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;

class IngredientEntityTypeConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> config)
    {
        config.ToTable("Ingredients", BeFitDbContext.DEFAULT_SCHEMA);

        config.HasKey(o => o.Id);

        config.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        config.Property(e => e.IsEnabled)
            .IsRequired();
    }
}
