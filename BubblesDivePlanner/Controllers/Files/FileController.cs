public class FileController : IFileController
{
    private const string FILE_NAME = "dive_planner.json";
    private readonly ISerialiser<ICylinderSelector> cylinderSelectorSerialiser;
    private readonly ISerialiser<IResult> resultSerialiser;

    public FileController(ISerialiser<ICylinderSelector> cylinderSelectorSerialiser, ISerialiser<IResult> resultSerialiser)
    {
        this.cylinderSelectorSerialiser = cylinderSelectorSerialiser;
        this.resultSerialiser = resultSerialiser;
    }

    public void Write(ICylinderSelector cylinderSelector, IResult result)
    {
        try
        {
            using StreamWriter cylinderSelectorWriter = new("cylinders.json");

            string serialisedCylinderSelector = cylinderSelectorSerialiser.Write(cylinderSelector);
            cylinderSelectorWriter.Write(serialisedCylinderSelector);
            
            using StreamWriter resultWriter = new("results.json");

            string serialisedResult = resultSerialiser.Write(result);
            resultWriter.Write(serialisedResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}

public interface IFileController
{
    void Write(ICylinderSelector cylinderSelector, IResult result);
}