using ReactiveUI;
using Xunit;

public class GasMixtureShould
{
    [Fact]
    public void HaveDefaults()
    {
        // Given
        GasMixture gasMixture = new();

        // Then
        Assert.Equal(100.0F, gasMixture.Nitrogen);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        GasMixture gasMixture = new();
        List<string> events = new();
        gasMixture.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        gasMixture.Oxygen = 21;
        gasMixture.Helium = 10;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(gasMixture);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(gasMixture.Oxygen), events);
        Assert.Contains(nameof(gasMixture.Helium), events);
    }

    [Theory]
    [InlineData(21.0F, 0.0F, 79.0F)]
    [InlineData(0.0F, 10.0F, 90.0F)]
    [InlineData(21.0F, 10.0F, 69.0F)]
    public void CalculateNitrogen(float oxygen, float helium, float nitrogen)
    {
        // Given
        GasMixture gasMixture = new()
        {
            Oxygen = oxygen,
            Helium = helium
        };

        // Then
        Assert.Equal(nitrogen, gasMixture.Nitrogen);
    }
}
