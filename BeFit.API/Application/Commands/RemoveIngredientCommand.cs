namespace BeFit.API.Application.Commands;

using MediatR;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using System.Runtime.Serialization;

[DataContract]
public record RemoveIngredientCommand
: IRequest<BaseDataResponse<IngredientResponseDTO>>
{
    [DataMember]
    public int Id { get; init; }
}