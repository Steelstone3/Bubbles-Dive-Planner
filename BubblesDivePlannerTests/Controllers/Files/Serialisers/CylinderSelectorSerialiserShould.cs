using System.Runtime.InteropServices;
using Xunit;

public class CylinderSelectorSerialiserShould
{
    [Fact]
    public void Construct()
    {
        // Given
        CylinderSelectorSerialiser cylinderSelectorSerialiser = new();

        // Then
        Assert.IsAssignableFrom<ISerialiser<ICylinderSelector>>(cylinderSelectorSerialiser);
    }

    [SkippableFact]
    public void Write()
    {
        Skip.If(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

        // Given
        // TODO AH Mock
        GasMixture gasMixture = new(new GasMixtureValidator(), new CylinderController(), new DiveBoundaryController())
        {
            Oxygen = 21
        };
        GasUsage gasUsage = new(new GasUsageValidator())
        {
            Remaining = 1680,
            Used = 720,
            SurfaceAirConsumptionRate = 12,
        };
        Cylinder cylinder = new(new CylinderValidator(), new CylinderController())
        {
            Name = "Air",
            Volume = 12,
            Pressure = 200,
            InitialPressurisedVolume = 2400,
            GasMixture = gasMixture,
            GasUsage = gasUsage,
        };

        CylinderSelector cylinderSelector = new()
        {
            SetupCylinder = cylinder,
            SelectedCylinder = cylinder,
        };

        cylinderSelector.Cylinders.Add(cylinder);

        CylinderSelectorSerialiser cylinderSelectorSerialiser = new();

        // When
        string serialisedCylinderSelector = cylinderSelectorSerialiser.Write(cylinderSelector);

        // Then
        Assert.Equal("{\"Cylinders\":[{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}],\"SetupCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}},\"SelectedCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}}", serialisedCylinderSelector);
    }

    [Fact]
    public void Read()
    {
        // Given
        string json = "{\"Cylinders\":[{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}],\"SetupCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}},\"SelectedCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}}";
        CylinderSelectorSerialiser resultSerialiser = new();

        // When
        ICylinderSelector cylinderSelector = resultSerialiser.Read(json);

        // Then
        Assert.NotNull(cylinderSelector);
        Assert.NotNull(cylinderSelector.Cylinders);
        Assert.NotEmpty(cylinderSelector.Cylinders);
        Assert.NotNull(cylinderSelector.SetupCylinder);
        Assert.NotNull(cylinderSelector.SelectedCylinder);
    }
}