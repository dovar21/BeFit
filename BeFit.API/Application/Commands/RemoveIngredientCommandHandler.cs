namespace BeFit.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using BeFit.API.Application.Enums;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.API.Application.Extensions;

public class RemoveIngredientCommandHandler
: IRequestHandler<RemoveIngredientCommand, BaseDataResponse<IngredientResponseDTO>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public RemoveIngredientCommandHandler(IMediator mediator,
        IMapper mapper,
        IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<IngredientResponseDTO>> Handle(RemoveIngredientCommand message, CancellationToken cancellationToken)
    {
        //Find ingredient
        var ingredient = await _ingredientRepository.FindAsync(message.Id);

        if (ingredient == null)
            return BaseDataResponse<IngredientResponseDTO>.Fail(null, new ErrorModel(ErrorCode.IngredientNotFound.GetDisplayName()));

        //Remove
        ingredient.Disable();

        _ingredientRepository.Update(ingredient);

        //Save changes
        await _ingredientRepository.UnitOfWork
             .SaveEntitiesAsync(cancellationToken);

        return BaseDataResponse<IngredientResponseDTO>.Success(_mapper.Map<IngredientResponseDTO>(ingredient));

    }
}