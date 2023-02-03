namespace BeFit.API.Application.Queries;

using global::AutoMapper;
using BeFit.API.Application.Enums;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.API.Application.Extensions;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

public class IngredientQueries : IIngredientQueries
{
    private string _connectionString = string.Empty;

    private readonly IMapper _mapper;
    private readonly IIngredientRepository _ingredientRepository;

    public IngredientQueries(IMapper mapper, 
        IIngredientRepository ingredientRepository,
        IConfiguration configuration)
    {

        _connectionString = configuration.GetConnectionString("BeFitDb");

        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
    }
    public async Task<BaseDataResponse<IngredientResponseDTO>> FindAsync(int id)
    {
        var result = await _ingredientRepository.FindAsync(id);

        if (result == null)
            return BaseDataResponse<IngredientResponseDTO>.Fail(null, new ErrorModel(ErrorCode.IngredientNotFound.GetDisplayName()));

        return BaseDataResponse<IngredientResponseDTO>.Success(_mapper.Map<IngredientResponseDTO>(result));
    }
    //Get all ingredients via Dapper
    public async Task<BaseDataResponse<List<IngredientResponseDTO>>> ToListAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        //Get not deleted rows
        var result = await connection.QueryAsync<IngredientResponseDTO>("SELECT * FROM Ingredients where IsEnabled = 1");

        return BaseDataResponse<List<IngredientResponseDTO>>.Success(result.ToList());
    }
}