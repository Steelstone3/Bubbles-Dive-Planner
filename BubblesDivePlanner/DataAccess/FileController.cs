using DynamicData;

public class FileController : IFileController
{
    private readonly ISerialiser<CylinderSelector> cylinderSelectorSerialiser;
    private readonly ISerialiser<Result> resultSerialiser;

    public FileController(ISerialiser<CylinderSelector> cylinderSelectorSerialiser, ISerialiser<Result> resultSerialiser)
    {
        this.cylinderSelectorSerialiser = cylinderSelectorSerialiser;
        this.resultSerialiser = resultSerialiser;
    }

    public void Write(Main main)
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
    public void Read(Main main)
    {
        try
        {
            main.New(main);

            string resultsJson = System.IO.File.ReadAllText("results.json");
            Result result = resultSerialiser.Read(resultsJson);

            string cylindersJson = System.IO.File.ReadAllText("cylinders.json");
            CylinderSelector cylinderSelector = cylinderSelectorSerialiser.Read(cylindersJson);

            UpdateCylinderSelector(main, cylinderSelector);
            UpdateResult(main, result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private void UpdateCylinderSelector(Main main, CylinderSelector cylinderSelector)
    {
        main.DivePlan.CylinderSelector.SetupCylinder = cylinderSelector.SetupCylinder;
        main.DivePlan.CylinderSelector.SelectedCylinder = cylinderSelector.SelectedCylinder;
        // main.DivePlan.CylinderSelector.Cylinders.Clear();
        // main.DivePlan.CylinderSelector.Cylinders.AddRange(cylinderSelector.Cylinders);
    }

    private void UpdateResult(Main main, Result result)
    {
        DiveStage latestResult = result.Results.Last();

        main.DivePlan.DiveModelSelector.DiveModelSelected = latestResult.DiveModel;
        main.DivePlan.DiveStage = latestResult;
        main.Result.Results.Clear();
        main.Result.Results.AddRange(result.Results);
    }
}

public interface IFileController
{
    void Write(Main main);
    void Read(Main main);
}