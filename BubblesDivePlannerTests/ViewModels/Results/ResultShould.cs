using Moq;
using Xunit;

public class ResultShould
{
    [Fact]
    public void Construct()
    {
        // Given
        Result result = new();

        // Then
        Assert.NotNull(result.Results);
        Assert.Empty(result.Results);
    }

    [Fact]
    public void CollectionChangedEvents()
    {
        // Given
        DiveStage diveStage = new();
        Result result = new();
        List<string> events = new();
        result.Results.CollectionChanged += (sender, e) => events.Add(e.NewItems.ToString());

        // When
        result.Results.Add(diveStage);

        // Then
        Assert.NotNull(result.Results);
        Assert.NotEmpty(result.Results);
        Assert.NotEmpty(events);
    }
}
