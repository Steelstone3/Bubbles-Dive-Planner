using Moq;
using Xunit;

public class DivePlanShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveModel> diveModel = new();
        Mock<IDiveStep> diveStep = new();
        DivePlan divePlan = new();
        List<string> events = new();
        divePlan.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        divePlan.DiveModel = diveModel.Object;
        divePlan.DiveStep = diveStep.Object;

        // Then
        Assert.NotEmpty(events);
        Assert.Contains(nameof(divePlan.DiveModel), events);
        Assert.Contains(nameof(divePlan.DiveStep), events);
    }
}