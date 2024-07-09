using System.Text.Json;
using System.Xml;

public class ResultSerialiser : ISerialiser<IResult>
{
    public string Write(IResult result)
    {
        string json = JsonSerializer.Serialize(result);
        
        return json;
    }

    public IResult Read(string json)
    {
        throw new NotImplementedException();
    }

}