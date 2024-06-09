using Moq;
using Xunit;

public class CylinderValidatorShould
{
    [Theory]
    [InlineData("A", 3, 50)]
    [InlineData("Air", 12, 200)]
    [InlineData("Air", 30, 300)]
    public void ValidateValidCylinder(string name, byte volume, ushort pressure)
    {
        // Given
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gm => gm.IsValid).Returns(true);
        Mock<IGasUsage> gasUsage = new();
        gasUsage.Setup(gu => gu.IsValid).Returns(true);
        Mock<ICylinder> cylinder = new();
        cylinder.Setup(c => c.Name).Returns(name);
        cylinder.Setup(c => c.Volume).Returns(volume);
        cylinder.Setup(c => c.Pressure).Returns(pressure);
        cylinder.Setup(c => c.GasMixture).Returns(gasMixture.Object);
        cylinder.Setup(c => c.GasUsage).Returns(gasUsage.Object);
        CylinderValidator cylinderValidator = new();

        // When
        bool isValid = cylinderValidator.Validate(cylinder.Object);

        // Then
        Assert.True(isValid);
    }

    [Theory]
    [InlineData(null, 0, 0)]
    [InlineData(" ", 0, 0)]
    [InlineData(null, 12, 200)]
    [InlineData(" ", 12, 200)]
    [InlineData("Air", 2, 200)]
    [InlineData("Air", 31, 200)]
    [InlineData("Air", 12, 49)]
    [InlineData("Air", 12, 301)]
    public void ValidateInvalidCylinder(string name, byte volume, ushort pressure)
    {
        // Given
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gm => gm.IsValid).Returns(true);
        Mock<IGasUsage> gasUsage = new();
        gasUsage.Setup(gu => gu.IsValid).Returns(true);
        Mock<ICylinder> cylinder = new();
        cylinder.Setup(c => c.Name).Returns(name);
        cylinder.Setup(c => c.Volume).Returns(volume);
        cylinder.Setup(c => c.Pressure).Returns(pressure);
        cylinder.Setup(c => c.GasMixture).Returns(gasMixture.Object);
        cylinder.Setup(c => c.GasUsage).Returns(gasUsage.Object);
        CylinderValidator cylinderValidator = new();

        // When
        bool isValid = cylinderValidator.Validate(cylinder.Object);

        // Then
        Assert.False(isValid);
    }
}