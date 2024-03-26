using Moq;
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
        Assert.NotNull(main.DivePlan);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDivePlan> divePlan = new();
        Main main = new();
        List<string> events = new();
        main.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        main.DivePlan = divePlan.Object;

        // Then
        Assert.NotEmpty(events);
        Assert.Contains(nameof(main.DivePlan), events);
    }
}
