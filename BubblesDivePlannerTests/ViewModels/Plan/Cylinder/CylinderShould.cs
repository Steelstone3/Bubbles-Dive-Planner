using Moq;
using ReactiveUI;
using Xunit;

public class CylinderShould
{
    [Fact]
    public void Constructs()
    {
        // Given
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        Cylinder cylinder = new(cylinderValidator.Object, cylinderController.Object);

        // Then
        Assert.IsAssignableFrom<IValidation>(cylinder);
        Assert.IsAssignableFrom<IVisibility>(cylinder);
        Assert.True(cylinder.IsVisible);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IGasUsage> gasUsage = new();
        Mock<IGasMixture> gasMixture = new();
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        Cylinder cylinder = new(cylinderValidator.Object, cylinderController.Object);
        List<string> events = new();
        cylinder.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        cylinder.Name = "EAN32";
        cylinder.Volume = 12;
        cylinder.Pressure = 200;
        cylinder.InitialPressurisedVolume = 2400;
        cylinder.GasUsage = gasUsage.Object;
        cylinder.GasMixture = gasMixture.Object;
        cylinder.IsVisible = false;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(cylinder);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(cylinder.Name), events);
        Assert.Contains(nameof(cylinder.Volume), events);
        Assert.Contains(nameof(cylinder.Pressure), events);
        Assert.Contains(nameof(cylinder.GasUsage), events);
        Assert.Contains(nameof(cylinder.GasMixture), events);
        Assert.Contains(nameof(cylinder.IsVisible), events);
    }

    [Fact]
    public void CalculateInitialPressurisedVolume()
    {
        // Given
        byte volume = 12;
        ushort pressure = 200;
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        cylinderController.Setup(cc => cc.CalculateInitialPressurisedVolume(volume, pressure));
        ICylinder cylinder = new Cylinder(cylinderValidator.Object, cylinderController.Object)
        {
            Volume = volume,
            Pressure = pressure
        };

        // Then
        cylinderController.VerifyAll();
    }

    [Fact]
    public void Validate()
    {
        // Given
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        ICylinder cylinder = new Cylinder(cylinderValidator.Object, cylinderController.Object);
        cylinderValidator.Setup(cv => cv.Validate(cylinder)).Returns(true);

        // When
        bool isValid = cylinder.IsValid;

        // Then
        Assert.True(isValid);
        cylinderValidator.VerifyAll();
    }
}