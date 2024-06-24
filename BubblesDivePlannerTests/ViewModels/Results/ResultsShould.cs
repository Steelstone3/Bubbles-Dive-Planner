using Moq;
using ReactiveUI;
using Xunit;

public class ResultsShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveStage> diveStage = new();
        Results results = new();
        List<string> events = new();
        results.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        results.LatestResult = diveStage.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(results);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(results.LatestResult), events);
    }
}
