using System.Text.Json;

public class ResultSerialiser : ISerialiser<IResult>
{
    public string Write(IResult result) => JsonSerializer.Serialize(result);

    public IResult Read(string json)
    {
        throw new NotImplementedException();
    }

}