using Moq;
using ReactiveUI;
using Xunit;

public class DiveModelShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveModelProfile> diveModelProfile = new();
        DiveModel diveModel = new();
        List<string> events = new();
        diveModel.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveModel.DiveModelProfile = diveModelProfile.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveModel);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveModel.DiveModelProfile), events);
    }
}