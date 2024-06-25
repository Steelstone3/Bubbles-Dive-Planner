using Moq;
using Xunit;

public class ResultShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveStage> diveStage = new();
        Result result = new();
        List<string> events = new();
        result.Results.CollectionChanged += (sender, e) => events.Add(e.NewItems.ToString());

        // When
        result.Results.Add(diveStage.Object);

        // Then
        Assert.NotNull(result.Results);
        Assert.NotEmpty(result.Results);
        Assert.NotEmpty(events);
    }
}