namespace BeFit.API.Application.Commands;

using System.Runtime.Serialization;

[DataContract]
public record UpdateIngredientCommand : CreateIngredientCommand
{
    [DataMember]
    public int Id { get; init; }
}
