public class FileController : IFileController
{
    private const string FILE_NAME = "dive_planner.json";
    private readonly IJsonController jsonController;

    public FileController(IJsonController jsonController)
    {
        this.jsonController = jsonController;
    }

    public void Save(IResult result)
    {
        try
        {
            using StreamWriter writer = new(FILE_NAME);
            string serialisedResult = jsonController.Serialise(result);
            writer.Write(serialisedResult);
        }
        catch (Exception)
        {
        }
    }
}

public interface IFileController
{
    void Save(IResult result);
}