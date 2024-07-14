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

            UpdateCylinderSelector(main, cylinderSelector);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        try
        {
            string json = System.IO.File.ReadAllText("results.json");
            IResult result = resultSerialiser.Read(json);

            UpdateResult(main, result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private void UpdateCylinderSelector(IMain main, ICylinderSelector cylinderSelector)
    {
        // Setup Cylinder
        main.DivePlan.CylinderSelector.SetupCylinder.Name = cylinderSelector.SetupCylinder.Name;
        main.DivePlan.CylinderSelector.SetupCylinder.Volume = cylinderSelector.SetupCylinder.Volume;
        main.DivePlan.CylinderSelector.SetupCylinder.Pressure = cylinderSelector.SetupCylinder.Pressure;
        main.DivePlan.CylinderSelector.SetupCylinder.InitialPressurisedVolume = cylinderSelector.SetupCylinder.InitialPressurisedVolume;
        main.DivePlan.CylinderSelector.SetupCylinder.GasMixture.Oxygen = cylinderSelector.SetupCylinder.GasMixture.Oxygen;
        main.DivePlan.CylinderSelector.SetupCylinder.GasMixture.Helium = cylinderSelector.SetupCylinder.GasMixture.Helium;
        main.DivePlan.CylinderSelector.SetupCylinder.GasUsage.Remaining = cylinderSelector.SetupCylinder.GasUsage.Remaining;
        main.DivePlan.CylinderSelector.SetupCylinder.GasUsage.Used = cylinderSelector.SetupCylinder.GasUsage.Used;
        main.DivePlan.CylinderSelector.SetupCylinder.GasUsage.SurfaceAirConsumptionRate = cylinderSelector.SetupCylinder.GasUsage.SurfaceAirConsumptionRate;

        // Selected Cylinder
        main.DivePlan.CylinderSelector.SelectedCylinder.Name = cylinderSelector.SelectedCylinder.Name;
        main.DivePlan.CylinderSelector.SelectedCylinder.Volume = cylinderSelector.SelectedCylinder.Volume;
        main.DivePlan.CylinderSelector.SelectedCylinder.Pressure = cylinderSelector.SelectedCylinder.Pressure;
        main.DivePlan.CylinderSelector.SelectedCylinder.InitialPressurisedVolume = cylinderSelector.SelectedCylinder.InitialPressurisedVolume;
        main.DivePlan.CylinderSelector.SelectedCylinder.GasMixture.Oxygen = cylinderSelector.SelectedCylinder.GasMixture.Oxygen;
        main.DivePlan.CylinderSelector.SelectedCylinder.GasMixture.Helium = cylinderSelector.SelectedCylinder.GasMixture.Helium;
        main.DivePlan.CylinderSelector.SelectedCylinder.GasUsage.Remaining = cylinderSelector.SelectedCylinder.GasUsage.Remaining;
        main.DivePlan.CylinderSelector.SelectedCylinder.GasUsage.Used = cylinderSelector.SelectedCylinder.GasUsage.Used;
        main.DivePlan.CylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate = cylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate;

        // Cylinders
        main.DivePlan.CylinderSelector.Cylinders.Clear();
        main.DivePlan.CylinderSelector.Cylinders.AddRange(cylinderSelector.Cylinders);
    }

    private void UpdateResult(IMain main, IResult result)
    {
        IDiveStage latestResult = result.Results.Last();

        // Dive Model
        main.DivePlan.DiveModelSelector.DiveModelSelected.Name = latestResult.DiveModel.Name;
        main.DivePlan.DiveModelSelector.DiveModelSelected.CompartmentCount = latestResult.DiveModel.CompartmentCount;
        main.DivePlan.DiveModelSelector.DiveModelSelected.NitrogenHalfTime = latestResult.DiveModel.NitrogenHalfTime;
        main.DivePlan.DiveModelSelector.DiveModelSelected.HeliumHalfTime = latestResult.DiveModel.HeliumHalfTime;
        main.DivePlan.DiveModelSelector.DiveModelSelected.AValuesNitrogen = latestResult.DiveModel.AValuesNitrogen;
        main.DivePlan.DiveModelSelector.DiveModelSelected.BValuesNitrogen = latestResult.DiveModel.BValuesNitrogen;
        main.DivePlan.DiveModelSelector.DiveModelSelected.AValuesHelium = latestResult.DiveModel.AValuesHelium;
        main.DivePlan.DiveModelSelector.DiveModelSelected.BValuesHelium = latestResult.DiveModel.BValuesHelium;

        // Dive Model Profile
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.OxygenAtPressure = latestResult.DiveModel.DiveModelProfile.OxygenAtPressure;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.NitrogenAtPressure = latestResult.DiveModel.DiveModelProfile.NitrogenAtPressure;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.HeliumAtPressure = latestResult.DiveModel.DiveModelProfile.HeliumAtPressure;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.NitrogenTissuePressures = latestResult.DiveModel.DiveModelProfile.NitrogenTissuePressures;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.HeliumTissuePressures = latestResult.DiveModel.DiveModelProfile.HeliumTissuePressures;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.TotalTissuePressures = latestResult.DiveModel.DiveModelProfile.TotalTissuePressures;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.AValues = latestResult.DiveModel.DiveModelProfile.AValues;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.BValues = latestResult.DiveModel.DiveModelProfile.BValues;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.ToleratedAmbientPressures = latestResult.DiveModel.DiveModelProfile.ToleratedAmbientPressures;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.MaxSurfacePressures = latestResult.DiveModel.DiveModelProfile.MaxSurfacePressures;
        main.DivePlan.DiveModelSelector.DiveModelSelected.DiveModelProfile.CompartmentLoads = latestResult.DiveModel.DiveModelProfile.CompartmentLoads;

        // Dive Stage: Dive Model
        main.DivePlan.DiveStage.DiveModel.Name = latestResult.DiveModel.Name;
        main.DivePlan.DiveStage.DiveModel.CompartmentCount = latestResult.DiveModel.CompartmentCount;
        main.DivePlan.DiveStage.DiveModel.NitrogenHalfTime = latestResult.DiveModel.NitrogenHalfTime;
        main.DivePlan.DiveStage.DiveModel.HeliumHalfTime = latestResult.DiveModel.HeliumHalfTime;
        main.DivePlan.DiveStage.DiveModel.AValuesNitrogen = latestResult.DiveModel.AValuesNitrogen;
        main.DivePlan.DiveStage.DiveModel.BValuesNitrogen = latestResult.DiveModel.BValuesNitrogen;
        main.DivePlan.DiveStage.DiveModel.AValuesHelium = latestResult.DiveModel.AValuesHelium;
        main.DivePlan.DiveStage.DiveModel.BValuesHelium = latestResult.DiveModel.BValuesHelium;

        // Dive Stage: Dive Model Profile
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.OxygenAtPressure = latestResult.DiveModel.DiveModelProfile.OxygenAtPressure;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.NitrogenAtPressure = latestResult.DiveModel.DiveModelProfile.NitrogenAtPressure;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.HeliumAtPressure = latestResult.DiveModel.DiveModelProfile.HeliumAtPressure;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures = latestResult.DiveModel.DiveModelProfile.NitrogenTissuePressures;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.HeliumTissuePressures = latestResult.DiveModel.DiveModelProfile.HeliumTissuePressures;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.TotalTissuePressures = latestResult.DiveModel.DiveModelProfile.TotalTissuePressures;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.AValues = latestResult.DiveModel.DiveModelProfile.AValues;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.BValues = latestResult.DiveModel.DiveModelProfile.BValues;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.ToleratedAmbientPressures = latestResult.DiveModel.DiveModelProfile.ToleratedAmbientPressures;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.MaxSurfacePressures = latestResult.DiveModel.DiveModelProfile.MaxSurfacePressures;
        main.DivePlan.DiveStage.DiveModel.DiveModelProfile.CompartmentLoads = latestResult.DiveModel.DiveModelProfile.CompartmentLoads;

        // Dive Stage: Dive Step
        main.DivePlan.DiveStage.DiveStep.Depth = latestResult.DiveStep.Depth;
        main.DivePlan.DiveStage.DiveStep.Time = latestResult.DiveStep.Time;

        // Dive Stage: Cylinder
        main.DivePlan.DiveStage.Cylinder.Name = latestResult.Cylinder.Name;
        main.DivePlan.DiveStage.Cylinder.Volume = latestResult.Cylinder.Volume;
        main.DivePlan.DiveStage.Cylinder.Pressure = latestResult.Cylinder.Pressure;
        main.DivePlan.DiveStage.Cylinder.InitialPressurisedVolume = latestResult.Cylinder.InitialPressurisedVolume;
        main.DivePlan.DiveStage.Cylinder.GasMixture.Oxygen = latestResult.Cylinder.GasMixture.Oxygen;
        main.DivePlan.DiveStage.Cylinder.GasMixture.Helium = latestResult.Cylinder.GasMixture.Helium;
        main.DivePlan.DiveStage.Cylinder.GasUsage.Remaining = latestResult.Cylinder.GasUsage.Remaining;
        main.DivePlan.DiveStage.Cylinder.GasUsage.Used = latestResult.Cylinder.GasUsage.Used;
        main.DivePlan.DiveStage.Cylinder.GasUsage.SurfaceAirConsumptionRate = latestResult.Cylinder.GasUsage.SurfaceAirConsumptionRate;

        // Results
        main.Result.Results.Clear();
        main.Result.Results.AddRange(result.Results);
    }
}

public interface IFileController
{
    void Write(IMain main);
    void Read(IMain main);
}