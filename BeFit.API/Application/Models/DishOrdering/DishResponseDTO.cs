namespace BeFit.API.Application.Models.DishOrdering;

using System;
using System.Text.Json.Serialization;

public class DishResponseDTO
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int[] Ingredients { get; private set; }
}