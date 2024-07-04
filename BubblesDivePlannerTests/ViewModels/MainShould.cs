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
    }
}
