using System.Text.Json.Serialization;

public interface IValidation
{
    [JsonIgnore]
    bool IsValid { get; }
}