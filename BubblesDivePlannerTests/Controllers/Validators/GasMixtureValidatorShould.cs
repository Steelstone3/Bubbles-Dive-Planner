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
        GasMixture gasMixture = new()
        {
            Oxygen = oxygen,
            Helium = helium,
            Nitrogen = nitrogen
        };
        GasMixtureValidator gasMixtureValidator = new();

        // When
        bool isValid = gasMixtureValidator.IsValid(gasMixture);

        // Then
        Assert.True(isValid);
    }
}