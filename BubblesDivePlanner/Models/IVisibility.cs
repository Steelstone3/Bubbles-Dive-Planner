using System.Text.Json.Serialization;

public interface IVisibility
{
    [JsonIgnore]
    bool IsVisible { get; set; }
}