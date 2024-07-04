using Moq;
using ReactiveUI;
using Xunit;

public class MainShould
{
    [Fact]
    public void Construct()
    {
        // Given
        Main main = new();

        // Then
        Assert.IsAssignableFrom<IMain>(main);
        Assert.NotNull(main.Header);
        Assert.NotNull(main.DivePlan);
        Assert.NotNull(main.DiveInformation);
        Assert.NotNull(main.Result);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IHeader> header = new();
        Mock<IDivePlan> divePlan = new();
        Mock<IDiveInformation> diveInformation = new();
        Mock<IResult> results = new();
        Main main = new();
        List<string> events = new();
        main.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        main.Header = header.Object;
        main.DivePlan = divePlan.Object;
        main.DiveInformation = diveInformation.Object;
        main.Result = results.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(main);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(main.Header), events);
        Assert.Contains(nameof(main.DivePlan), events);
        Assert.Contains(nameof(main.DiveInformation), events);
        Assert.Contains(nameof(main.Result), events);
    }

    [Fact]
    public void CalculateDiveStage()
    {
        // Given
        Main main = new();
        main.DivePlan.DiveModelSelector.DiveModelSelected = new Zhl16Buhlmann();
        main.DivePlan.DiveStage.DiveStep.Depth = 50;
        main.DivePlan.DiveStage.DiveStep.Time = 50;
        main.DivePlan.CylinderSelector.SelectedCylinder = main.DivePlan.CylinderSelector.SetupCylinder;
        main.DivePlan.CylinderSelector.SelectedCylinder.Name = "Air";
        main.DivePlan.CylinderSelector.SelectedCylinder.Volume = 12;
        main.DivePlan.CylinderSelector.SelectedCylinder.Pressure = 200;
        main.DivePlan.CylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate = 12;
        main.DivePlan.CylinderSelector.SelectedCylinder.GasMixture.Oxygen = 21;
        main.DivePlan.CylinderSelector.Cylinders.Add(main.DivePlan.CylinderSelector.SelectedCylinder);

        // When
        main.CalculateCommand.Execute().Subscribe();

        // Then
        Assert.NotEmpty(main.Result.Results);
        Assert.NotEmpty(main.DiveInformation.DecompressionProfile.DecompressionSteps);

        // Given
        // const byte COMPARTMENT_COUNT = 16;
        // Mock<ICylinderController> cylinderController = new();
        // Mock<ICylinderValidator> cylinderValidator = new();
        // Mock<IDiveStageValidator> diveStageValidator = new();
        // DiveStage diveStage = new(diveStageValidator.Object)
        // {
        //     DiveModel = new Zhl16Buhlmann()
        //     {
        //         DiveModelProfile = new DiveModelProfile(COMPARTMENT_COUNT, new DiveBoundaryController())
        //         {
        //             OxygenAtPressure = 1.26f,
        //             NitrogenAtPressure = 4.74f,
        //             HeliumAtPressure = 0.0f,

        //             NitrogenTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f },
        //             HeliumTissuePressures = new float[COMPARTMENT_COUNT] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f },
        //             TotalTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f },
        //             AValues = new float[COMPARTMENT_COUNT] { 1.2559f, 1.0000f, 0.8618f, 0.7562f, 0.6667f, 0.5600f, 0.4947f, 0.4500f, 0.4187f, 0.3798f, 0.3497f, 0.3223f, 0.2850f, 0.2737f, 0.2523f, 0.2327f },
        //             BValues = new float[COMPARTMENT_COUNT] { 0.5050f, 0.6514f, 0.7222f, 0.7825f, 0.8126f, 0.8434f, 0.8693f, 0.8910f, 0.9092f, 0.9222f, 0.9319f, 0.9403f, 0.9477f, 0.9544f, 0.9602f, 0.9653f },
        //             ToleratedAmbientPressures = new float[COMPARTMENT_COUNT] { 1.4068f, 1.3544f, 1.1624f, 0.9923f, 0.8269f, 0.7455f, 0.6682f, 0.6059f, 0.5589f, 0.5471f, 0.5442f, 0.5459f, 0.5627f, 0.5592f, 0.5687f, 0.5794f },
        //             MaxSurfacePressures = new float[COMPARTMENT_COUNT] { 3.2401f, 2.5352f, 2.2465f, 2.0342f, 1.8973f, 1.7457f, 1.6451f, 1.5723f, 1.5186f, 1.4642f, 1.4228f, 1.3858f, 1.3402f, 1.3215f, 1.2937f, 1.2686f },
        //             CompartmentLoads = new float[COMPARTMENT_COUNT] { 124.74f, 121.46f, 110.01f, 99.51f, 88.77f, 82.71f, 76.8f, 71.87f, 68.05f, 66.46f, 65.62f, 65.15f, 65.57f, 65.05f, 65.29f, 65.66f },
        //         }
        //     },
        //     Cylinder = new Cylinder(cylinderValidator.Object, cylinderController.Object)
        // };
        // DecompressionController decompressionController = new();

        // When
        // List<IDiveStep> decompressionSteps = decompressionController.CollateDecompressionDiveSteps(diveStage);

        // Then
        // Assert.NotNull(decompressionSteps);
        // Assert.NotEmpty(decompressionSteps);
        // Assert.Equal(6, decompressionSteps[0].Depth);
        // Assert.Equal(1, decompressionSteps[0].Time);
        // Assert.Equal(3, decompressionSteps[1].Depth);
        // Assert.Equal(4, decompressionSteps[1].Time);
    }
}
