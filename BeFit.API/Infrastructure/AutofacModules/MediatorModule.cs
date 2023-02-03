namespace BeFit.API.Infrastructure.AutofacModules;

using Autofac;
using MediatR;
using BeFit.API.Application.Commands;
using System.Reflection;

public class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(typeof(CreateDishCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));
        builder.RegisterAssemblyTypes(typeof(UpdateDishCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));
        builder.RegisterAssemblyTypes(typeof(RemoveDishCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));

        builder.Register<ServiceFactory>(context =>
        {
            var componentContext = context.Resolve<IComponentContext>();
            return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
        });
    }
}
