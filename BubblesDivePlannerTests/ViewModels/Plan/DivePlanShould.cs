using Moq;
using ReactiveUI;
using Xunit;

public class DivePlanShould
{
    [Fact]
    public void Construct()
    {
        // Given
        DivePlan divePlan = new();

        // Then
        Assert.NotNull(divePlan.DiveModelSelector);
        Assert.NotNull(divePlan.CylinderSelector);
        Assert.NotNull(divePlan.DiveStage);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveModelSelector> diveModelSelector = new();
        Mock<ICylinderSelector> cylinderSelector = new();
        Mock<IDiveStage> diveStage = new();
        DivePlan divePlan = new();
        List<string> events = new();
        divePlan.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        divePlan.DiveModelSelector = diveModelSelector.Object;
        divePlan.CylinderSelector = cylinderSelector.Object;
        divePlan.DiveStage = diveStage.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(divePlan);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(divePlan.DiveModelSelector), events);
        Assert.Contains(nameof(divePlan.CylinderSelector), events);
        Assert.Contains(nameof(divePlan.DiveStage), events);
    }
}