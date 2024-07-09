public class FileController : IFileController
{
    private const string FILE_NAME = "dive_planner.json";
    private readonly ISerialiser<IResult> resultSerialiser;

    public FileController(ISerialiser<IResult> resultSerialiser)
    {
        this.resultSerialiser = resultSerialiser;
    }

    public void Write(IResult result)
    {
        try
        {
            using StreamWriter writer = new(FILE_NAME);
            string serialisedResult = resultSerialiser.Write(result);
            writer.Write(serialisedResult);
        }
        catch (Exception)
        {
        }
    }
}

public interface IFileController
{
    void Write(IResult result);
}