namespace BeFit.API.Controllers;

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BeFit.API.Application.Commands;
using BeFit.API.Application.Models;
using BeFit.API.Application.Queries;
using BeFit.API.Application.Models.DishOrdering;

[Route("api/[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IIngredientQueries _ingredientQueries;

    public IngredientsController(IMapper mapper,
        IMediator mediator,
        IIngredientQueries ingredientQueries)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _ingredientQueries = ingredientQueries ?? throw new ArgumentNullException(nameof(ingredientQueries));
    }

    [HttpGet]
    public async Task<BaseDataResponse<List<IngredientResponseDTO>>> GetIngredientsAsync()
    {
        return await _ingredientQueries.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<BaseDataResponse<IngredientResponseDTO>> GetIngredientByIdAsync(int id)
    {
        return await _ingredientQueries.FindAsync(id);
    }
    [HttpPost]
    public async Task<BaseDataResponse<IngredientResponseDTO>> CreateAsync([FromBody] CreateIngredientCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    public async Task<BaseDataResponse<IngredientResponseDTO>> UpdateAsync([FromBody] UpdateIngredientCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id:int}")]
    public async Task<BaseDataResponse<IngredientResponseDTO>> RemoveAsync(int id)
    {
        return await _mediator.Send(new RemoveIngredientCommand { Id = id });
    }
}