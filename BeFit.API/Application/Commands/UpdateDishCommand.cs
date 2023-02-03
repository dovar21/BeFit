namespace BeFit.API.Application.Commands;

using System.Runtime.Serialization;

[DataContract]
public record UpdateDishCommand : CreateDishCommand
{
    [DataMember]
    public int Id { get; init; }
}
