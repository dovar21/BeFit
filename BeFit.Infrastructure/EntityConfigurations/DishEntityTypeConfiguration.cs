namespace BeFit.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;

class DishEntityTypeConfiguration : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> config)
    {
        config.ToTable("Dishes", BeFitDbContext.DEFAULT_SCHEMA);

        config.HasKey(o => o.Id);

        config.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        config.Property(e => e.IsEnabled)
            .IsRequired();
    }
}