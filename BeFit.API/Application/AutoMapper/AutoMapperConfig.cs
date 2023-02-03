
namespace BeFit.API.Application.AutoMapper;

using global::AutoMapper;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DishOrderingMappingProfile());
        });
    }
}