using System.Text.Json;

public class CylinderSelectorSerialiser : ISerialiser<ICylinderSelector>
{
    public string Write(ICylinderSelector cylinderSelector) => JsonSerializer.Serialize(cylinderSelector);
    public ICylinderSelector Read(string json) => JsonSerializer.Deserialize<ICylinderSelector>(json);
}