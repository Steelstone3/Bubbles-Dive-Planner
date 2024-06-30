using Moq;
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
        Assert.IsAssignableFrom<ICylinderSelector>(cylinderSelector);
        Assert.NotNull(cylinderSelector.SetupCylinder);
        Assert.Null(cylinderSelector.SelectedCylinder);
        Assert.Empty(cylinderSelector.Cylinders);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<ICylinder> cylinder = new();
        CylinderSelector cylinderSelector = new();
        List<string> events = new();
        cylinderSelector.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        cylinderSelector.SetupCylinder = cylinder.Object;
        cylinderSelector.SelectedCylinder = cylinder.Object;

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
        Mock<ICylinder> cylinder = new();
        CylinderSelector cylinderSelector = new();
        List<string> events = new();
        cylinderSelector.Cylinders.CollectionChanged += (sender, e) => events.Add(e.NewItems.ToString());

        // When
        cylinderSelector.Cylinders.Add(cylinder.Object);

        // Then
        Assert.NotEmpty(events);
    }

    [Fact]
    public void SelectedCylinderChangedEvents()
    {
        // Given
        Mock<ICylinder> cylinder = new();
        CylinderSelector cylinderSelector = new();
        string eventMessage = "Cylinder Changed";
        List<string> events = new();
        cylinderSelector.SelectedCylinderChanged += () => events.Add(eventMessage);

        // When
        cylinderSelector.SelectedCylinder = cylinder.Object;

        // Then
        Assert.Contains(eventMessage, events);
    }

    [Fact]
    public void AddCylinder()
    {
        // Given
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        ICylinder cylinder = new Cylinder(cylinderValidator.Object, cylinderController.Object);
        cylinderValidator.Setup(cv => cv.Validate(cylinder)).Returns(true);
        CylinderSelector cylinderSelector = new()
        {
            SetupCylinder = cylinder
        };

        // When
        cylinderSelector.AddCylinderCommand.Execute().Subscribe();

        // Then
        Assert.NotEmpty(cylinderSelector.Cylinders);
    }
}