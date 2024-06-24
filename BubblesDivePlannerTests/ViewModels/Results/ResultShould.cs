using Moq;
using ReactiveUI;
using Xunit;

public class ResultShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveStage> diveStage = new();
        Result results = new();
        List<string> events = new();
        results.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        results.Results = diveStage.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(results);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(results.Results), events);
    }
}
