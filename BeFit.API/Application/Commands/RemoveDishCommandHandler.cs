namespace BeFit.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using BeFit.API.Application.Enums;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.API.Application.Extensions;

public class RemoveDishCommandHandler
: IRequestHandler<RemoveDishCommand, BaseDataResponse<DishResponseDTO>>
{
    private readonly IDishRepository _dishRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public RemoveDishCommandHandler(IMediator mediator,
        IMapper mapper,
        IDishRepository dishRepository)
    {
        _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<DishResponseDTO>> Handle(RemoveDishCommand message, CancellationToken cancellationToken)
    {
        //Find dish
        var dish = await _dishRepository.FindAsync(message.Id);

        if (dish == null)
            return BaseDataResponse<DishResponseDTO>.Fail(null, new ErrorModel(ErrorCode.DishNotFound.GetDisplayName()));

        //Remove
        dish.Disable();

        _dishRepository.Update(dish);

        //Save changes
        await _dishRepository.UnitOfWork
             .SaveEntitiesAsync(cancellationToken);

        return BaseDataResponse<DishResponseDTO>.Success(_mapper.Map<DishResponseDTO>(dish));

    }
}