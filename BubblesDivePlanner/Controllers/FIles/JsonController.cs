public class JsonController : IJsonController
{
    public string Serialise(IResult result)
    {
        throw new NotImplementedException();
    }
}

public interface IJsonController
{
    string Serialise(IResult result);
}