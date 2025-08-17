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
        DiveModelSelector diveModelSelector = new();
        CylinderSelector cylinderSelector = new();
        DiveStage diveStage = new();
        DivePlan divePlan = new();
        List<string> events = new();
        divePlan.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        divePlan.DiveModelSelector = diveModelSelector;
        divePlan.CylinderSelector = cylinderSelector;
        divePlan.DiveStage = diveStage;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(divePlan);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(divePlan.DiveModelSelector), events);
        Assert.Contains(nameof(divePlan.CylinderSelector), events);
        Assert.Contains(nameof(divePlan.DiveStage), events);
    }
}