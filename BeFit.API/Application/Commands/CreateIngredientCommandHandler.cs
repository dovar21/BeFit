namespace BeFit.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using BeFit.API.Application.Enums;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.Specifications.DishOrdering;
using Microsoft.OpenApi.Extensions;

public class CreateIngredientCommandHandler
: IRequestHandler<CreateIngredientCommand, BaseDataResponse<IngredientResponseDTO>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateIngredientCommandHandler(IMediator mediator,
        IMapper mapper,
        IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<IngredientResponseDTO>> Handle(CreateIngredientCommand message, CancellationToken cancellationToken)
    {
        //Check ingredient uniqueness
        if (await _ingredientRepository.FindAsync(
                new IngredientUniquenessCheckSpecification(
                    0,
                    message.Name)) != null
            )
            return BaseDataResponse<IngredientResponseDTO>.Fail(null, new ErrorModel(ErrorCode.IngredientNameMustBeUnique.GetDisplayName()));

        //Create new ingredient
        var ingredient = new Ingredient(message.Name);

        ingredient.Enable();

        //Add
        var result = _ingredientRepository.Add(ingredient);

        //Save changes
        await _ingredientRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);

        return BaseDataResponse<IngredientResponseDTO>.Success(_mapper.Map<IngredientResponseDTO>(result));

    }
}
