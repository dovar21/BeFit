namespace BeFit.API.Application.Commands;

using MediatR;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using System.Runtime.Serialization;

[DataContract]
public record CreateIngredientCommand
: IRequest<BaseDataResponse<IngredientResponseDTO>>
{
    [DataMember]
    public string Name { get; init; }
    public CreateIngredientCommand()
    {
       
    }
    public CreateIngredientCommand(string name): base()
    {
        Name = name;
    }
}