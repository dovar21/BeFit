namespace BeFit.API.Application.Commands;

using MediatR;
using BeFit.API.Application.Models;
using BeFit.API.Application.Models.DishOrdering;
using System.Runtime.Serialization;

[DataContract]
public record RemoveDishCommand
: IRequest<BaseDataResponse<DishResponseDTO>>
{
    [DataMember]
    public int Id { get; init; }
}