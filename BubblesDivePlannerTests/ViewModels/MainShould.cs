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
        main.DivePlan = divePlan.Object;
        main.DiveInformation = diveInformation.Object;
        main.Result = results.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(main);
        Assert.NotEmpty(events);
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
        main.DivePlan.DiveStage.DiveStep.Time = 10;
        main.DivePlan.CylinderSelector.SetupCylinder.Name = "Air";
        main.DivePlan.CylinderSelector.SetupCylinder.Volume = 12;
        main.DivePlan.CylinderSelector.SetupCylinder.Pressure = 200;
        main.DivePlan.CylinderSelector.SetupCylinder.GasUsage.SurfaceAirConsumptionRate = 12;
        main.DivePlan.CylinderSelector.SetupCylinder.GasMixture.Oxygen = 21;
        main.DivePlan.CylinderSelector.SelectedCylinder = main.DivePlan.CylinderSelector.SetupCylinder;
        main.DivePlan.CylinderSelector.Cylinders.Add(main.DivePlan.CylinderSelector.SelectedCylinder);

        // When
        main.CalculateCommand.Execute().Subscribe();

        // Then
        Assert.NotEmpty(main.Result.Results);
        Assert.NotEmpty(main.DiveInformation.DecompressionProfile.DecompressionSteps);
    }

    [Fact]
    public void UpdateDecompressionSteps()
    {
        // Given
        Main main = new();
        main.DivePlan.DiveModelSelector.DiveModelSelected = new Zhl16Buhlmann();
        main.DivePlan.DiveStage.DiveStep.Depth = 50;
        main.DivePlan.DiveStage.DiveStep.Time = 10;

        main.DivePlan.CylinderSelector.SetupCylinder.Name = "Air";
        main.DivePlan.CylinderSelector.SetupCylinder.Volume = 12;
        main.DivePlan.CylinderSelector.SetupCylinder.Pressure = 200;
        main.DivePlan.CylinderSelector.SetupCylinder.GasUsage.SurfaceAirConsumptionRate = 12;
        main.DivePlan.CylinderSelector.SetupCylinder.GasMixture.Oxygen = 21;

        main.DivePlan.CylinderSelector.SelectedCylinder = main.DivePlan.CylinderSelector.SetupCylinder;
        main.DivePlan.CylinderSelector.Cylinders.Add(main.DivePlan.CylinderSelector.SelectedCylinder);

        main.DivePlan.CylinderSelector.SelectedCylinder = main.DivePlan.CylinderSelector.Cylinders[0];

        // When
        main.CalculateCommand.Execute().Subscribe();

        // Then
        Assert.Equal(6, main.DiveInformation.DecompressionProfile.DecompressionSteps[0].Depth);
        Assert.Equal(1, main.DiveInformation.DecompressionProfile.DecompressionSteps[0].Time);
        Assert.Equal(3, main.DiveInformation.DecompressionProfile.DecompressionSteps[1].Depth);
        Assert.Equal(3, main.DiveInformation.DecompressionProfile.DecompressionSteps[1].Time);

        // Given
        main.DivePlan.CylinderSelector.SetupCylinder.Name = "EAN50";
        main.DivePlan.CylinderSelector.SetupCylinder.GasMixture.Oxygen = 50;

        main.DivePlan.CylinderSelector.SelectedCylinder = main.DivePlan.CylinderSelector.SetupCylinder;
        main.DivePlan.CylinderSelector.Cylinders.Add(main.DivePlan.CylinderSelector.SelectedCylinder);

        main.DivePlan.CylinderSelector.SelectedCylinder = main.DivePlan.CylinderSelector.Cylinders[1];

        // Then
        Assert.Equal(6, main.DiveInformation.DecompressionProfile.DecompressionSteps[0].Depth);
        Assert.Equal(1, main.DiveInformation.DecompressionProfile.DecompressionSteps[0].Time);
        Assert.Equal(3, main.DiveInformation.DecompressionProfile.DecompressionSteps[1].Depth);
        Assert.Equal(2, main.DiveInformation.DecompressionProfile.DecompressionSteps[1].Time);
    }
}
