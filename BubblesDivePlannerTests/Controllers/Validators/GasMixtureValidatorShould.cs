using Moq;
using Xunit;

public class GasMixtureValidatorShould
{
    [Theory]
    [InlineData(21.0F, 0.0F, 79.0F)]
    [InlineData(21.0F, 10.0F, 69.0F)]
    [InlineData(5.0F, 0.0F, 95.0F)]
    [InlineData(5.0F, 10.0F, 85.0F)]
    public void ValidateValidGasMixtures(float oxygen, float helium, float nitrogen)
    {
        // Given
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gasMixture => gasMixture.Oxygen).Returns(oxygen);
        gasMixture.Setup(gasMixture => gasMixture.Helium).Returns(helium);
        gasMixture.Setup(gasMixture => gasMixture.Nitrogen).Returns(nitrogen);
        GasMixtureValidator gasMixtureValidator = new();

        // When
        bool isValid = gasMixtureValidator.Validate(gasMixture.Object);

        // Then
        Assert.True(isValid);
    }

    [Theory]
    [InlineData(0.0F, 0.0F, 100.0F)]
    [InlineData(0.0F, 10.0F, 90.0F)]
    [InlineData(100.0F, 1.0F, 0.0F)]
    [InlineData(1.0F, 100.0F, 0.0F)]
    [InlineData(4.0F, 0.0F, 96.0F)]
    public void ValidateInvalidGasMixtures(float oxygen, float helium, float nitrogen)
    {
        // Given
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gasMixture => gasMixture.Oxygen).Returns(oxygen);
        gasMixture.Setup(gasMixture => gasMixture.Helium).Returns(helium);
        gasMixture.Setup(gasMixture => gasMixture.Nitrogen).Returns(nitrogen);
        GasMixtureValidator gasMixtureValidator = new();

        // When
        bool isValid = gasMixtureValidator.Validate(gasMixture.Object);

        // Then
        Assert.False(isValid);
    }
}