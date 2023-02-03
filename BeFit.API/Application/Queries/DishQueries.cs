namespace BeFit.API.Application.Queries;

using global::AutoMapper;
using BeFit.API.Application.Enums;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.API.Application.Extensions;

public class DishQueries : IDishQueries
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public DishQueries(IMapper mapper, 
        IDishRepository dishRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
    }
    public async Task<BaseDataResponse<DishResponseDTO>> FindAsync(int id)
    {
        var result = await _dishRepository.FindAsync(id);

        if (result == null)
            return BaseDataResponse<DishResponseDTO>.Fail(null, new ErrorModel(ErrorCode.DishNotFound.GetDisplayName()));

        return BaseDataResponse<DishResponseDTO>.Success(_mapper.Map<DishResponseDTO>(result));
    }
    public async Task<BaseDataResponse<List<DishResponseDTO>>> ToListAsync()
    {
        var result = await _dishRepository.ToListAsync();
        
        return BaseDataResponse<List<DishResponseDTO>>.Success(_mapper.Map<List<DishResponseDTO>>(result));
    }
}