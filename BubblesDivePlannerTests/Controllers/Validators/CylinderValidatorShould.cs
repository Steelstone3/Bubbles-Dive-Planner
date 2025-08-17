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
        GasMixture gasMixture = new()
        {
            Oxygen = 21,
            Helium = 10,
            Nitrogen = 69
        };
        GasUsage gasUsage = new()
        {
            Remaining = 1680,
            Used = 720,
            SurfaceAirConsumptionRate = 12,
        };
        Cylinder cylinder = new()
        {
            Name = name,
            Volume = volume,
            Pressure = pressure,
            GasMixture = gasMixture,
            GasUsage = gasUsage,
        };
        CylinderValidator cylinderValidator = new();

        // When
        bool isValid = cylinderValidator.IsValid(cylinder);

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
        GasMixture gasMixture = new()
        {
            Oxygen = 21,
            Helium = 10,
            Nitrogen = 69
        };

        GasUsage gasUsage = new()
        {
            Remaining = 1680,
            Used = 720,
            SurfaceAirConsumptionRate = 12,
        };
        Cylinder cylinder = new()
        {
            Name = name,
            Volume = volume,
            Pressure = pressure,
            GasMixture = gasMixture,
            GasUsage = gasUsage
        };
        CylinderValidator cylinderValidator = new();

        // When
        bool isValid = cylinderValidator.IsValid(cylinder);

        // Then
        Assert.False(isValid);
    }
}