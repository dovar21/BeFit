namespace BeFit.Domain.AggregatesModel.DishOrderingAggregates;

using BeFit.Domain.SeedWork;

public class DishIngredient
    : Entity
{
    public Dish Dish { get; private set; }
    public int DishId { get; private set; }
    public Ingredient Ingredient { get; private set; }
    public int IngredientId { get; private set; }

    public DishIngredient(int dishId, int ingredientId)
    {
        DishId = dishId;
        IngredientId = ingredientId;
    }
}
