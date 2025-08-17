using Moq;
using ReactiveUI;
using Xunit;

public class GasMixtureShould
{
    private readonly Mock<ICylinderController> cylinderController = new();
    private readonly Mock<IDiveBoundaryController> diveBoundaryController = new();

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
        gasMixture.Nitrogen = 79;
        gasMixture.MaximumOperatingDepth = 56.66F;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(gasMixture);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(gasMixture.Oxygen), events);
        Assert.Contains(nameof(gasMixture.Helium), events);
        Assert.Contains(nameof(gasMixture.Nitrogen), events);
        Assert.Contains(nameof(gasMixture.MaximumOperatingDepth), events);
    }

    [Fact]
    public void DeepClone()
    {
        // Given
        GasMixture gasMixture = new();

        // When
        GasMixture clonedGasMixture = new(gasMixture);

        // Then
        Assert.NotSame(gasMixture, clonedGasMixture);
    }
}
