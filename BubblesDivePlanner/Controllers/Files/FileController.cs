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
            using StreamWriter writer = new(FILE_NAME);

            string serialised = cylinderSelectorSerialiser.Write(cylinderSelector);
            // TODO AH Hacky string manipulation to create valid json
            serialised = serialised.Remove(serialised.Length - 1);
            serialised += ",";

            string serialisedResult = resultSerialiser.Write(result);
            serialisedResult = serialisedResult.Remove(0, 1);
            serialised += serialisedResult;

            writer.WriteLine(serialised);
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