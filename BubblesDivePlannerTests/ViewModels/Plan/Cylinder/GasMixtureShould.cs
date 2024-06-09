using Moq;
using ReactiveUI;
using Xunit;

public class GasMixtureShould
{
    private readonly Mock<IGasMixtureValidator> gasMixtureValidator = new();
    private readonly Mock<ICylinderController> cylinderController = new();

    [Fact]
    public void Constructs()
    {
        // Given
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object);

        // Then
        Assert.IsAssignableFrom<IValidation>(gasMixture);
        Assert.IsAssignableFrom<IVisibility>(gasMixture);
        Assert.False(gasMixture.IsVisible);
        Assert.Equal(100.0F, gasMixture.Nitrogen);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object);
        List<string> events = new();
        gasMixture.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        gasMixture.Oxygen = 21;
        gasMixture.Helium = 10;
        gasMixture.IsVisible = true;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(gasMixture);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(gasMixture.Oxygen), events);
        Assert.Contains(nameof(gasMixture.Helium), events);
        Assert.Contains(nameof(gasMixture.IsVisible), events);
    }

    [Fact]
    public void CalculateNitrogen()
    {
        // Given
        float oxygen = 21;
        float helium = 10;
        
        Mock<ICylinderController> cylinderController = new();
        cylinderController.Setup(cc => cc.CalculateNitrogen(oxygen, helium));
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object)
        {
            Oxygen = oxygen,
            Helium = helium
        };

        // Then
        cylinderController.VerifyAll();
    }

    [Fact]
    public void Validate()
    {
        // Given
        Mock<IGasMixtureValidator> gasMixtureValidator = new();
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object);
        gasMixtureValidator.Setup(gmv => gmv.Validate(gasMixture)).Returns(true);

        // When
        bool isValid = gasMixture.IsValid;

        // Then
        Assert.True(isValid);
        gasMixtureValidator.VerifyAll();
    }
}
