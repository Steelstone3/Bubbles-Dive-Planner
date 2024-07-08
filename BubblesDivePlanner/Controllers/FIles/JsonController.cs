using Newtonsoft.Json;

public class JsonController : IJsonController
{
    public string Serialise(IResult result)
    {
        return JsonConvert.SerializeObject(result.Results, Formatting.Indented);
    }
}

public interface IJsonController
{
    string Serialise(IResult result);
}