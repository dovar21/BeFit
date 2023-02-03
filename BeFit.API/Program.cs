using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BeFit.API.Application.AutoMapper;
using BeFit.API.Controllers;
using BeFit.API.Infrastructure.AutofacModules;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(DishesController).Assembly)
            .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true)
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<BeFit.Infrastructure.BeFitDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BeFitDb"), b => b.MigrationsAssembly("BeFit.API"));
    },
    ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
);

var mappingConfig = AutoMapperConfig.RegisterMappings();

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers().AddJsonOptions(opts => {
    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(cb=> cb.RegisterModule(new MediatorModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(cb=> cb.RegisterModule(new ApplicationModule(builder.Configuration.GetConnectionString("BeFitDb"))));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.Run();
