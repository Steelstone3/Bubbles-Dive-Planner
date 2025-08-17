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

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(gasMixture);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(gasMixture.Oxygen), events);
        Assert.Contains(nameof(gasMixture.Helium), events);
    }

    // TODO AH Uses controller. Needs to be outside the model.
    [Fact]
    public void CalculateNitrogen()
    {
        // Given
        float oxygen = 21;
        float helium = 10;

        GasMixture gasMixture = new()
        {
            Oxygen = oxygen,
            Helium = helium
        };

        // Then
        Assert.Equal(69.0, gasMixture.Nitrogen);
    }

    // TODO AH Uses controller. Needs to be outside the model.
    [Fact]
    public void CalculateMaximumOperatingDepth()
    {
        // Given
        float oxygen = 21.0F;
        float maximumOperatingDepth = 56.6666666666F;
        diveBoundaryController.Setup(db => db.CalculateMaximumOperatingDepth(oxygen)).Returns(maximumOperatingDepth);
        GasMixture gasMixture = new()
        {
            Oxygen = oxygen
        };

        // Then
        Assert.Equal(maximumOperatingDepth, gasMixture.MaximumOperatingDepth, 0.01);
    }
}
