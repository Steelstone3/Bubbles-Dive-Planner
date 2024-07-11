using System.Text.Json;

public class ResultSerialiser : ISerialiser<IResult>
{
    public string Write(IResult result)
    {
        DalResult dalResult = new DalResultConverter().ConvertTo(result);

        return JsonSerializer.Serialize(dalResult);
    }

    public IResult Read(string json)
    {
        return JsonSerializer.Deserialize<IResult>(json);
    }

    //  public string Write(ICylinderSelector cylinderSelector)
    // {
    //     DalCylinderSelector dalCylinderSelector = new DalCylinderSelectorConverter().ConvertTo(cylinderSelector);

    //     return JsonSerializer.Serialize(dalCylinderSelector);
    // }

    // public ICylinderSelector Read(string json)
    // {
    //     DalCylinderSelector dalCylinderSelector = JsonSerializer.Deserialize<DalCylinderSelector>(json);

    //     return new DalCylinderSelectorConverter().ConvertFrom(dalCylinderSelector);
    // }
}