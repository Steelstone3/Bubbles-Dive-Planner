using Moq;
using ReactiveUI;
using Xunit;

public class GasMixtureShould
{
    private readonly Mock<IGasMixtureValidator> gasMixtureValidator = new();
    private readonly Mock<ICylinderController> cylinderController = new();
    private readonly Mock<IDiveBoundaryController> diveBoundaryController = new();

    [Fact]
    public void Construct()
    {
        // Given
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object, diveBoundaryController.Object);

        // Then
        Assert.IsAssignableFrom<IGasMixture>(gasMixture);
        Assert.IsAssignableFrom<IValidation>(gasMixture);
        Assert.Equal(100.0F, gasMixture.Nitrogen);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object, diveBoundaryController.Object);
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

    [Fact]
    public void CalculateNitrogen()
    {
        // Given
        float oxygen = 21;
        float helium = 10;

        Mock<ICylinderController> cylinderController = new();
        cylinderController.Setup(cc => cc.CalculateNitrogen(oxygen, helium));
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object, diveBoundaryController.Object)
        {
            Oxygen = oxygen,
            Helium = helium
        };

        // Then
        cylinderController.VerifyAll();
    }

    [Fact]
    public void CalculateMaximumOperatingDepth()
    {
        // Given
        float oxygen = 21.0F;
        float maximumOperatingDepth = 56.6F;
        Mock<IDiveBoundaryController> diveBoundaryController = new();
        diveBoundaryController.Setup(db => db.CalculateMaximumOperatingDepth(oxygen)).Returns(maximumOperatingDepth);
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object, diveBoundaryController.Object)
        {
            Oxygen = oxygen
        };

        // Then
        diveBoundaryController.VerifyAll();
        Assert.Equal(gasMixture.MaximumOperatingDepth, maximumOperatingDepth);
    }

    [Fact]
    public void Validate()
    {
        // Given
        Mock<IGasMixtureValidator> gasMixtureValidator = new();
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object, diveBoundaryController.Object);
        gasMixtureValidator.Setup(gmv => gmv.Validate(gasMixture)).Returns(true);

        // When
        bool isValid = gasMixture.IsValid;

        // Then
        Assert.True(isValid);
        gasMixtureValidator.VerifyAll();
    }
}
