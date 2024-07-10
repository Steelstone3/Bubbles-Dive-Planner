using System.Text.Json;
using System.Text.Json.Serialization;

public class CylinderSelectorSerialiser : ISerialiser<ICylinderSelector>
{
    public string Write(ICylinderSelector cylinderSelector) => JsonSerializer.Serialize(cylinderSelector);
    public ICylinderSelector Read(string json) => JsonSerializer.Deserialize<ICylinderSelector>(json);
}