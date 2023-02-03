using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;

namespace BeFit.API.Application.Queries;

public interface IDishQueries
{
    Task<BaseDataResponse<DishResponseDTO>> FindAsync(int id);
    Task<BaseDataResponse<List<DishResponseDTO>>> ToListAsync();
}