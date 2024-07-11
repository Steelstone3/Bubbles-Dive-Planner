using System.Text.Json;

public class CylinderSelectorSerialiser : ISerialiser<ICylinderSelector>
{
    public string Write(ICylinderSelector cylinderSelector)
    {
        DalCylinderSelector dalCylinderSelector = new DalCylinderSelectorConverter().ConvertTo(cylinderSelector);

        return JsonSerializer.Serialize(dalCylinderSelector);
    }

    public ICylinderSelector Read(string json)
    {
        DalCylinderSelector dalCylinderSelector = JsonSerializer.Deserialize<DalCylinderSelector>(json);

        return new DalCylinderSelectorConverter().ConvertFrom(dalCylinderSelector);
    }
}