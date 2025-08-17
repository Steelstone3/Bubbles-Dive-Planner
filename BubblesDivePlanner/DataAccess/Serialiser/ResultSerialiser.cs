using System.Text.Json;

public class ResultSerialiser : ISerialiser<Result>
{
    public string Write(Result result)
    {
        DalResult dalResult = new DalResultConverter().ConvertTo(result);

        return JsonSerializer.Serialize(dalResult);
    }

    public Result Read(string json)
    {
        DalResult dalResult = JsonSerializer.Deserialize<DalResult>(json);

        return new DalResultConverter().ConvertFrom(dalResult);
    }
}