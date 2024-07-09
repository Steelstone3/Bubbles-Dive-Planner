using System.Runtime.InteropServices;
using Xunit;

public class ResultSerialiserShould
{

    [Fact]
    public void Construct()
    {
        // Given
        ResultSerialiser resultSerialiser = new();

        // Then
        Assert.IsAssignableFrom<ISerialiser<IResult>>(resultSerialiser);
    }

    // [SkippableFact]
    [Fact]
    public void Write()
    {
        Skip.If(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

        // Given

        DiveStep diveStep = new(new DiveStepValidator())
        {
            Depth = 50,
            Time = 10,
        };
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
        DiveStage diveStage = new(new DiveStageValidator())
        {
            DiveModel = new Zhl16Buhlmann(),
            DiveStep = diveStep,
            Cylinder = cylinder,
        };
        Result result = new();

        result.Results.Add(diveStage);

        ResultSerialiser resultSerialiser = new();

        // When
        string serialisedResult = resultSerialiser.Write(result);

        // Then
        Assert.Equal("{\"Results\":[{\"DiveModel\":{\"Name\":\"Zhl16-B Model\",\"CompartmentCount\":16,\"NitrogenHalfTime\":[4,8,12.5,18.5,27,38.3,54.3,77,109,146,187,239,305,390,498,635],\"HeliumHalfTime\":[1.51,3.02,4.72,6.99,10.21,14.48,20.53,29.11,41.2,55.19,70.69,90.34,115.29,147.42,188.24,240.03],\"AValuesNitrogen\":[1.2559,1,0.8618,0.7562,0.6667,0.56,0.4947,0.45,0.4187,0.3798,0.3497,0.3223,0.285,0.2737,0.2523,0.2327],\"BValuesNitrogen\":[0.505,0.6514,0.7222,0.7825,0.8126,0.8434,0.8693,0.891,0.9092,0.9222,0.9319,0.9403,0.9477,0.9544,0.9602,0.9653],\"AValuesHelium\":[1.7424,1.383,1.1919,1.0458,0.922,0.8205,0.7305,0.6502,0.595,0.5545,0.5333,0.5189,0.5181,0.5176,0.5172,0.5119],\"BValuesHelium\":[0.4245,0.5747,0.6527,0.7223,0.7582,0.7957,0.8279,0.8553,0.8757,0.8903,0.8997,0.9073,0.9122,0.9171,0.9217,0.9267],\"DiveModelProfile\":{\"OxygenAtPressure\":0,\"NitrogenAtPressure\":0,\"HeliumAtPressure\":0,\"NitrogenTissuePressures\":[0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79],\"HeliumTissuePressures\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"TotalTissuePressures\":[0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79,0.79],\"AValues\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"BValues\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"ToleratedAmbientPressures\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"MaxSurfacePressures\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"CompartmentLoads\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"DiveCeiling\":0}},\"DiveStep\":{\"Depth\":50,\"Time\":10,\"IsValid\":true},\"Cylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0,\"Nitrogen\":79,\"MaximumOperatingDepth\":56.67,\"IsValid\":true},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12,\"IsValid\":true},\"IsValid\":true,\"IsVisible\":true},\"IsValid\":true}]}", serialisedResult);
    }
}