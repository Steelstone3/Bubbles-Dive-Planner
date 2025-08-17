using System.Text.Json;

public class CylinderSelectorSerialiser : ISerialiser<CylinderSelector>
{
    public string Write(CylinderSelector cylinderSelector)
    {
        DalCylinderSelector dalCylinderSelector = new DalCylinderSelectorConverter().ConvertTo(cylinderSelector);

        return JsonSerializer.Serialize(dalCylinderSelector);
    }

    public CylinderSelector Read(string json)
    {
        DalCylinderSelector dalCylinderSelector = JsonSerializer.Deserialize<DalCylinderSelector>(json);

        return new DalCylinderSelectorConverter().ConvertFrom(dalCylinderSelector);
    }
}