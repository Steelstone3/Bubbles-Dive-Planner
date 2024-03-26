using Moq;
using Xunit;

public class GasMixtureValidatorShould
{
     [Theory]
    [InlineData(21.0F, 0.0F, 79.0F)]
    [InlineData(0.0F, 10.0F, 90.0F)]
    [InlineData(21.0F, 10.0F, 69.0F)]
    public void CalculateNitrogen(float oxygen, float helium, float expectedNitrogen)
    {
        // Given
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gasMixture => gasMixture.Oxygen).Returns(oxygen);
        gasMixture.Setup(gasMixture => gasMixture.Helium).Returns(helium);
        GasMixtureValidator gasMixtureValidator = new();

        float nitrogen = gasMixtureValidator.CalculateNitrogen(gasMixture.Object);
        

        // Then
        Assert.Equal(expectedNitrogen, nitrogen);
    }
}