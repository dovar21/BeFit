namespace BeFit.API.Application.Enums;

using System.ComponentModel;

public enum ErrorCode
{
    [Description("Dish not found")]
    DishNotFound = 1,
    [Description("Dish name must be unique ")]
    DishNameMustBeUnique = 2,
    [Description("Ingredient not found")]
    IngredientNotFound = 3,
    [Description("Ingredient name must be unique ")]
    IngredientNameMustBeUnique = 4,
    [Description("No ingredient selected")]
    NoIngredientSelected = 4,
}