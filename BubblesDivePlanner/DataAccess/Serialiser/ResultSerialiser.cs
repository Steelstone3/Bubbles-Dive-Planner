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
        DalResult dalResult = JsonSerializer.Deserialize<DalResult>(json);

        return new DalResultConverter().ConvertFrom(dalResult);
    }
}