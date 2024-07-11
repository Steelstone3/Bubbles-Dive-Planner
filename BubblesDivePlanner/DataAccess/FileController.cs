using System.IO;
using DynamicData;

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
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        try
        {
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
        try
        {
            string json = System.IO.File.ReadAllText("cylinders.json");
            ICylinderSelector cylinderSelector = cylinderSelectorSerialiser.Read(json);

            main.DivePlan.CylinderSelector.SetupCylinder = cylinderSelector.SetupCylinder;
            main.DivePlan.CylinderSelector.SelectedCylinder = cylinderSelector.SelectedCylinder;
            main.DivePlan.CylinderSelector.Cylinders.Clear();
            main.DivePlan.CylinderSelector.Cylinders.AddRange(cylinderSelector.Cylinders);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        try
        {
            string json = System.IO.File.ReadAllText("results.json");
            IResult result = resultSerialiser.Read(json);

            IDiveStage latestResult = result.Results.Last();

            main.DivePlan.DiveModelSelector.DiveModelSelected = latestResult.DiveModel;
            main.DivePlan.DiveStage = latestResult;
            main.Result = result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}

public interface IFileController
{
    void Write(IMain main);
    void Read(IMain main);
}