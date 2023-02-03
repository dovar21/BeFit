namespace BeFit.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using BeFit.API.Application.Enums;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.Specifications.DishOrdering;
using BeFit.API.Application.Extensions;

public class UpdateIngredientCommandHandler
: IRequestHandler<UpdateIngredientCommand, BaseDataResponse<IngredientResponseDTO>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateIngredientCommandHandler(IMediator mediator,
        IMapper mapper,
        IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<IngredientResponseDTO>> Handle(UpdateIngredientCommand message, CancellationToken cancellationToken)
    {
        //Find ingredient
        var ingredient = await _ingredientRepository.FindAsync(message.Id);

        if (ingredient == null)
            return BaseDataResponse<IngredientResponseDTO>.Fail(null, new ErrorModel(ErrorCode.IngredientNotFound.GetDisplayName()));

        //Check ingredient uniqueness
        if (await _ingredientRepository.FindAsync(
                new IngredientUniquenessCheckSpecification(
                    message.Id,
                    message.Name)) != null
            )
            return BaseDataResponse<IngredientResponseDTO>.Fail(null, new ErrorModel(ErrorCode.IngredientNameMustBeUnique.GetDisplayName()));

        //Update name
        ingredient.SetName(message.Name);

        //Update
        _ingredientRepository.Update(ingredient);

        //Save changes
        await _ingredientRepository.UnitOfWork
             .SaveEntitiesAsync(cancellationToken);

        return BaseDataResponse<IngredientResponseDTO>.Success(_mapper.Map<IngredientResponseDTO>(ingredient));

    }
}
