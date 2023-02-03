namespace BeFit.API.Controllers;

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BeFit.API.Application.Commands;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.API.Application.Queries;

[Route("api/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IDishQueries _dishQueries;

    public DishesController(IMapper mapper,
        IMediator mediator,
        IDishQueries dishQueries)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _dishQueries = dishQueries ?? throw new ArgumentNullException(nameof(dishQueries));
    }

    [HttpGet]
    public async Task<BaseDataResponse<List<DishResponseDTO>>> GetDishesAsync()
    {
        return await _dishQueries.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<BaseDataResponse<DishResponseDTO>> GetDishByIdAsync(int id)
    {
        return await _dishQueries.FindAsync(id);
    }
    [HttpPost]
    public async Task<BaseDataResponse<DishResponseDTO>> CreateAsync([FromBody] CreateDishCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    public async Task<BaseDataResponse<DishResponseDTO>> UpdateAsync([FromBody] UpdateDishCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id:int}")]
    public async Task<BaseDataResponse<DishResponseDTO>> RemoveAsync(int id)
    {
        return await _mediator.Send(new RemoveDishCommand { Id = id });
    }
}