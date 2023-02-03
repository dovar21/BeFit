namespace BeFit.API.Application.Commands;

using MediatR;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using System.Runtime.Serialization;

[DataContract]
public record CreateDishCommand
: IRequest<BaseDataResponse<DishResponseDTO>>
{
    [DataMember]
    public string Name { get; init; }

    [DataMember]
    public int[] Ingredients { get; init; }

    public CreateDishCommand()
    {
        
    }

    public CreateDishCommand(string name) : this()
    {
        Name = name;
    }

    public record IngredientDTO
    {
        public int Id { get; init; }
    }
}