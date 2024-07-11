public class FileController : IFileController
{
    private readonly ISerialiser<ICylinderSelector> cylinderSelectorSerialiser;
    private readonly ISerialiser<IResult> resultSerialiser;

    public FileController(ISerialiser<ICylinderSelector> cylinderSelectorSerialiser, ISerialiser<IResult> resultSerialiser)
    {
        this.cylinderSelectorSerialiser = cylinderSelectorSerialiser;
        this.resultSerialiser = resultSerialiser;
    }

    public void Write(IMain main)
    {
        try
        {
            using StreamWriter cylinderSelectorWriter = new("cylinders.json");

            string serialisedCylinderSelector = cylinderSelectorSerialiser.Write(main.DivePlan.CylinderSelector);
            cylinderSelectorWriter.Write(serialisedCylinderSelector);

            using StreamWriter resultWriter = new("results.json");

            string serialisedResult = resultSerialiser.Write(main.Result);
            resultWriter.Write(serialisedResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    // TODO AH Interaction Test
    public void Read(IMain main)
    {
        throw new NotImplementedException();
    }
}

public interface IFileController
{
    void Write(IMain main);
    void Read(IMain main);
}