namespace BeFit.API.Infrastructure.AutofacModules;

using Autofac;
using BeFit.API.Application.Queries;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Infrastructure.Repositories;

public class ApplicationModule : Module
{
    public string QueriesConnectionString { get; }

    public ApplicationModule(string qconstr)
    {
        QueriesConnectionString = qconstr;
    }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DishQueries>()
        .As<IDishQueries>()
        .InstancePerLifetimeScope();

        builder.RegisterType<IngredientQueries>()
       .As<IIngredientQueries>()
       .InstancePerLifetimeScope();

        builder.RegisterType<DishRepository>()
        .As<IDishRepository>()
        .InstancePerLifetimeScope();

        builder.RegisterType<IngredientRepository>()
       .As<IIngredientRepository>()
       .InstancePerLifetimeScope();
    }
}
