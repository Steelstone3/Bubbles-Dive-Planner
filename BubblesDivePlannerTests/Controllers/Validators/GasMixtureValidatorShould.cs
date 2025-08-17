using Xunit;

public class GasMixtureValidatorShould
{
    [Theory]
    [InlineData(21.0F, 0.0F)]
    [InlineData(21.0F, 10.0F)]
    [InlineData(5.0F, 0.0F)]
    [InlineData(5.0F, 10.0F)]
    public void ValidateValidGasMixtures(float oxygen, float helium)
    {
        // Given
        GasMixture gasMixture = new()
        {
            Oxygen = oxygen,
            Helium = helium,
        };
        GasMixtureValidator gasMixtureValidator = new();

        // When
        bool isValid = gasMixtureValidator.Validate(gasMixture);

        // Then
        Assert.True(isValid);
    }
}