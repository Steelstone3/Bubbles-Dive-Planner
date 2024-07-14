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
            using StreamWriter resultWriter = new("results.json");

            string serialisedResult = resultSerialiser.Write(main.Result);
            resultWriter.Write(serialisedResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

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
    }

    // TODO AH Interaction Test
    public void Read(IMain main)
    {
        try
        {
            MainPrototype mainPrototype = new();
            mainPrototype.NewInstance(main);

            string resultsJson = System.IO.File.ReadAllText("results.json");
            IResult result = resultSerialiser.Read(resultsJson);

            string cylindersJson = System.IO.File.ReadAllText("cylinders.json");
            ICylinderSelector cylinderSelector = cylinderSelectorSerialiser.Read(cylindersJson);

            UpdateCylinderSelector(main, cylinderSelector);
            UpdateResult(main, result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private void UpdateCylinderSelector(IMain main, ICylinderSelector cylinderSelector)
    {
        main.DivePlan.CylinderSelector.SetupCylinder = cylinderSelector.SetupCylinder;
        main.DivePlan.CylinderSelector.SelectedCylinder = cylinderSelector.SelectedCylinder;
        // main.DivePlan.CylinderSelector.Cylinders.Clear();
        // main.DivePlan.CylinderSelector.Cylinders.AddRange(cylinderSelector.Cylinders);
    }

    private void UpdateResult(IMain main, IResult result)
    {
        IDiveStage latestResult = result.Results.Last();

        main.DivePlan.DiveModelSelector.DiveModelSelected = latestResult.DiveModel;
        main.DivePlan.DiveStage = latestResult;
        main.Result.Results.Clear();
        main.Result.Results.AddRange(result.Results);
    }
}

public interface IFileController
{
    void Write(IMain main);
    void Read(IMain main);
}