using Moq;
using ReactiveUI;
using Xunit;

public class CylinderSelectorShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<ICylinder> cylinder = new();
        CylinderSelector cylinderSelector = new();
        List<string> events = new();
        cylinderSelector.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        cylinderSelector.SelectedCylinder = cylinder.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(cylinderSelector);
        Assert.NotEmpty(events);
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
    public void AddCylinder()
    {
        // Given
        Mock<ICylinder> cylinder = new();
        cylinder.Setup(c => c.IsValid).Returns(true);
        CylinderSelector cylinderSelector = new()
        {
            SelectedCylinder = cylinder.Object
        };

        // When
        cylinderSelector.AddCylinderCommand.Execute().Subscribe();

        // Then
        Assert.NotEmpty(cylinderSelector.Cylinders);
    }
}