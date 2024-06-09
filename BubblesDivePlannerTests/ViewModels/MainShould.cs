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
        Assert.NotNull(main.DiveModelSelector);
        Assert.NotNull(main.DiveStage);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveModelSelector> diveModelSelector = new();
        Mock<ICylinderSelector> cylinderSelector = new();
        Mock<IDiveStage> diveStage = new();
        Main main = new();
        List<string> events = new();
        main.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        main.DiveModelSelector = diveModelSelector.Object;
        main.CylinderSelector = cylinderSelector.Object;
        main.DiveStage = diveStage.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(main);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(main.DiveModelSelector), events);
        Assert.Contains(nameof(main.CylinderSelector), events);
        Assert.Contains(nameof(main.DiveStage), events);
    }

    [Fact(Skip = "To Do")]
    public void CalculateDiveStage()
    {
        // // Given
        // Mock<ICylinder> cylinder = new();
        // cylinder.Setup(c => c.IsValid).Returns(true);
        // CylinderSelector cylinderSelector = new()
        // {
        //     SelectedCylinder = cylinder.Object
        // };

        // // When
        // cylinderSelector.AddCylinderCommand.Execute().Subscribe();

        // // Then
        // Assert.NotEmpty(cylinderSelector.Cylinders);
    }
}
