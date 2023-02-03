namespace BeFit.Domain.AggregatesModel.DishOrderingAggregates;

using BeFit.Domain.SeedWork;

public class Ingredient: Entity, IAggregateRoot
{
    public string Name { get; private set; }
    private List<DishIngredient> _dishIngredients;
    public IReadOnlyCollection<DishIngredient> DishIngredients => _dishIngredients;
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

    public Ingredient(string name)
    {
        Name = name;
    }
    public void SetName(string name)
    {
        Name = name;
    }
}
