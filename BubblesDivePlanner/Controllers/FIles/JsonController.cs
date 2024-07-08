using Newtonsoft.Json;

public class JsonController : IJsonController
{
    public string Serialise(IMain main)
    {
        return JsonConvert.SerializeObject(main, Formatting.Indented);
    }
}

public interface IJsonController
{
    string Serialise(IMain main);
}