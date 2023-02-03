namespace BeFit.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using BeFit.API.Application.Enums;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using BeFit.Domain.AggregatesModel.DishOrderingAggregates;
using BeFit.Domain.Specifications.DishOrdering;
using BeFit.API.Application.Extensions;

public class UpdateDishCommandHandler
: IRequestHandler<UpdateDishCommand, BaseDataResponse<DishResponseDTO>>
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateDishCommandHandler(IMediator mediator,
        IMapper mapper,
        IDishRepository dishRepository,
        IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<DishResponseDTO>> Handle(UpdateDishCommand message, CancellationToken cancellationToken)
    {
        //Check ingredients
        if (message.Ingredients.Length == 0)
            return BaseDataResponse<DishResponseDTO>.Fail(null, new ErrorModel(ErrorCode.NoIngredientSelected.GetDisplayName()));

        //Find dish
        var dish = await _dishRepository.FindAsync(message.Id);

        if (dish == null)
            return BaseDataResponse<DishResponseDTO>.Fail(null, new ErrorModel(ErrorCode.DishNotFound.GetDisplayName()));

        //Check dish uniqueness
        if (await _dishRepository.FindAsync(
                new DishUniquenessCheckSpecification(
                    message.Id,
                    message.Name)) != null
            )
            return BaseDataResponse<DishResponseDTO>.Fail(null, new ErrorModel(ErrorCode.DishNameMustBeUnique.GetDisplayName()));


        //Update name
        dish.SetName(message.Name);

        //Clear dish ingredients before saving
        dish.ClearIngredients();

        //Add dish ingredients
        foreach (var id in message.Ingredients)
            dish.AddIngredient(dish.Id, id);

        //Update
        _dishRepository.Update(dish);

        //Save changes
        await _dishRepository.UnitOfWork
             .SaveEntitiesAsync(cancellationToken);

        return BaseDataResponse<DishResponseDTO>.Success(_mapper.Map<DishResponseDTO>(dish));

    }
}
