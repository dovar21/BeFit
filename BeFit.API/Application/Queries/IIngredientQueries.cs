using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;

namespace BeFit.API.Application.Queries;

public interface IIngredientQueries
{
    Task<BaseDataResponse<IngredientResponseDTO>> FindAsync(int id);
    Task<BaseDataResponse<List<IngredientResponseDTO>>> ToListAsync();
}