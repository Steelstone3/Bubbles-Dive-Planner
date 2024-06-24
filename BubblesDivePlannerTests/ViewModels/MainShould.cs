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
        Assert.NotNull(main.DivePlan);
        Assert.NotNull(main.Result);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDivePlan> divePlan = new();
        Mock<IResult> results = new();
        Main main = new();
        List<string> events = new();
        main.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        main.DivePlan = divePlan.Object;
        main.Result = results.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(main);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(main.DivePlan), events);
        Assert.Contains(nameof(main.Result), events);
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
