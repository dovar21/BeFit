
namespace BeFit.Domain.AggregatesModel.DishOrderingAggregates;

using BeFit.Domain.SeedWork;

public class Dish: Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public bool IsEnabled { get; private set; }
    //Enable
    public void Enable()
    {
        IsEnabled = true;
    }
    //Disable
    public void Disable()
    {
        IsEnabled = false;
    }

    private List<DishIngredient> _dishIngredients;
    public IReadOnlyCollection<DishIngredient> DishIngredients => _dishIngredients;

    protected Dish()
    {
        _dishIngredients = new();
    }

    public Dish(string name) : this()
    {
        Name = name;
    }

    public void AddIngredient(int dishId, int ingredientId)
    {
        var exist = _dishIngredients.Where(o => o.IngredientId == ingredientId)
            .SingleOrDefault();

        if (exist == null)
        {
            //Add new ingredient
            var ingredient = new DishIngredient(dishId, ingredientId);
            _dishIngredients.Add(ingredient);
        }
    }

    public void ClearIngredients()
    {
        _dishIngredients = new();
    }

    public void SetName(string name)
    {
        Name = name;
    }
}
