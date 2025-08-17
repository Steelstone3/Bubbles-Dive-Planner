using ReactiveUI;
using Xunit;

public class CylinderSelectorShould
{
    [Fact]
    public void Construct()
    {
        // Given
        CylinderSelector cylinderSelector = new();

        // Then
        Assert.NotNull(cylinderSelector.SetupCylinder);
        Assert.Null(cylinderSelector.SelectedCylinder);
        Assert.Empty(cylinderSelector.Cylinders);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Cylinder cylinder = new();
        CylinderSelector cylinderSelector = new();
        List<string> events = new();
        cylinderSelector.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        cylinderSelector.SetupCylinder = cylinder;
        cylinderSelector.SelectedCylinder = cylinder;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(cylinderSelector);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(cylinderSelector.SetupCylinder), events);
        Assert.Contains(nameof(cylinderSelector.SelectedCylinder), events);
    }

    [Fact]
    public void CollectionsChangedEvents()
    {
        // Given
        Cylinder cylinder = new();
        CylinderSelector cylinderSelector = new();
        List<string> events = new();
        cylinderSelector.Cylinders.CollectionChanged += (sender, e) => events.Add(e.NewItems.ToString());

        // When
        cylinderSelector.Cylinders.Add(cylinder);

        // Then
        Assert.NotEmpty(events);
    }

    [Fact]
    public void SelectedCylinderChangedEvents()
    {
        // Given
        Cylinder cylinder = new();
        CylinderSelector cylinderSelector = new();
        string eventMessage = "Cylinder Changed";
        List<string> events = new();
        cylinderSelector.SelectedCylinderChanged += () => events.Add(eventMessage);

        // When
        cylinderSelector.SelectedCylinder = cylinder;

        // Then
        Assert.Contains(eventMessage, events);
    }

    [Fact]
    public void AddCylinder()
    {
        // Given
        GasMixture gasMixture = new()
        {
            Oxygen = 21
        };
        GasUsage gasUsage = new()
        {
            Remaining = 2400,
            Used = 0,
            SurfaceAirConsumptionRate = 12,
        };
        Cylinder cylinder = new()
        {
            Name = "Air",
            Pressure = 200,
            Volume = 12,
            GasMixture = gasMixture,
            GasUsage = gasUsage,
        };
        CylinderSelector cylinderSelector = new()
        {
            SetupCylinder = cylinder
        };

        // When
        cylinderSelector.AddCylinderCommand.Execute().Subscribe();

        // Then
        Assert.NotEmpty(cylinderSelector.Cylinders);
    }

    [Fact]
    public void AddInvalidCylinder()
    {
        // Given
        GasMixture gasMixture = new()
        {
            Oxygen = 0
        };
        GasUsage gasUsage = new()
        {
            Remaining = 2400,
            Used = 0,
            SurfaceAirConsumptionRate = 0,
        };
        Cylinder cylinder = new()
        {
            Name = "Air",
            Pressure = 500,
            Volume = 1,
            GasMixture = gasMixture,
            GasUsage = gasUsage,
        };
        CylinderSelector cylinderSelector = new()
        {
            SetupCylinder = cylinder
        };

        // When
        cylinderSelector.AddCylinderCommand.Execute().Subscribe();

        // Then
        Assert.Empty(cylinderSelector.Cylinders);
    }
}