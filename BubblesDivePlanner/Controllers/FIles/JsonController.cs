using Newtonsoft.Json;

public class JsonController : IJsonController
{
    // ICylinderSelector
    public string Serialise(IResult result)
    {
        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }
}

public interface IJsonController
{
    string Serialise(IResult result);
}