namespace BeFit.API.Application.AutoMapper;

using global::AutoMapper;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;

public class DishOrderingMappingProfile : Profile
{
    public DishOrderingMappingProfile()
    {
        //Response
        CreateMap<Dish, DishResponseDTO>()
            .ForMember(vm => vm.Ingredients, opt => opt.MapFrom(src => src.DishIngredients.Select(m=>m.IngredientId)));

        CreateMap<Ingredient, IngredientResponseDTO>();
    }
}