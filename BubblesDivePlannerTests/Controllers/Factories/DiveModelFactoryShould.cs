using Moq;
using Xunit;

public class DiveModelFactoryShould
{
    [Fact]
    public void CreateUsnRevisionSix()
    {
        // Given
        const byte COMPARTMENT_COUNT = 9;
        string Name = "USN Revision 6 Model";
        float[] NitrogenHalfTime = new float[COMPARTMENT_COUNT] { 5.0F, 10.0F, 20.0F, 40.0F, 80.0F, 120.0F, 160.0F, 200.0F, 240.0F };
        float[] HeliumHalfTime = new float[COMPARTMENT_COUNT] { 5.0F, 10.0F, 20.0F, 40.0F, 80.0F, 120.0F, 160.0F, 200.0F, 240.0F };
        float[] AValuesNitrogen = new float[COMPARTMENT_COUNT] { 1.37F, 1.08F, 0.69F, 0.3F, 0.34F, 0.38F, 0.4F, 0.45F, 0.42F };
        float[] BValuesNitrogen = new float[COMPARTMENT_COUNT] { 0.555F, 0.625F, 0.666F, 0.714F, 0.769F, 0.833F, 0.870F, 0.909F, 0.909F };
        float[] AValuesHelium = new float[COMPARTMENT_COUNT] { 1.12F, 0.85F, 0.71F, 0.63F, 0.5F, 0.44F, 0.54F, 0.61F, 0.61F };
        float[] BValuesHelium = new float[COMPARTMENT_COUNT] { 0.67F, 0.714F, 0.769F, 0.83F, 0.83F, 0.91F, 1.0F, 1.0F, 1.0F };
        DiveModelProfile DiveModelProfile = new(COMPARTMENT_COUNT);
        IDiveModelFactory diveModelFactory = new DiveModelFactory();

        // When
        DiveModel usnRevisionSix = diveModelFactory.CreateUsnRevisionSix();

        // Then
        Assert.Equal(COMPARTMENT_COUNT, usnRevisionSix.CompartmentCount);
        Assert.Equal(Name, usnRevisionSix.Name);
        Assert.Equal(NitrogenHalfTime, usnRevisionSix.NitrogenHalfTime);
        Assert.Equal(HeliumHalfTime, usnRevisionSix.HeliumHalfTime);
        Assert.Equal(AValuesNitrogen, usnRevisionSix.AValuesNitrogen);
        Assert.Equal(BValuesNitrogen, usnRevisionSix.BValuesNitrogen);
        Assert.Equal(AValuesHelium, usnRevisionSix.AValuesHelium);
        Assert.Equal(BValuesHelium, usnRevisionSix.BValuesHelium);
        Assert.Equivalent(DiveModelProfile, usnRevisionSix.DiveModelProfile);
    }

     [Fact]
    public void Construct()
    {
        // Given
        const byte COMPARTMENT_COUNT = 16;
        string Name = "Zhl16-B Model";
        float[] NitrogenHalfTime = new float[COMPARTMENT_COUNT] { 4.0F, 8.0F, 12.5F, 18.5F, 27.0F, 38.3F, 54.3F, 77.0F, 109.0F, 146.0F, 187.0F, 239.0F, 305.0F, 390.0F, 498.0F, 635.0F };
        float[] HeliumHalfTime = new float[COMPARTMENT_COUNT] { 1.51F, 3.02F, 4.72F, 6.99F, 10.21F, 14.48F, 20.53F, 29.11F, 41.20F, 55.19F, 70.69F, 90.34F, 115.29F, 147.42F, 188.24F, 240.03F };
        float[] AValuesNitrogen = new float[COMPARTMENT_COUNT] { 1.2559F, 1.0000F, 0.8618F, 0.7562F, 0.6667F, 0.5600F, 0.4947F, 0.4500F, 0.4187F, 0.3798F, 0.3497F, 0.3223F, 0.2850F, 0.2737F, 0.2523F, 0.2327F };
        float[] BValuesNitrogen = new float[COMPARTMENT_COUNT] { 0.5050F, 0.6514F, 0.7222F, 0.7825F, 0.8126F, 0.8434F, 0.8693F, 0.8910F, 0.9092F, 0.9222F, 0.9319F, 0.9403F, 0.9477F, 0.9544F, 0.9602F, 0.9653F };
        float[] AValuesHelium = new float[COMPARTMENT_COUNT] { 1.7424F, 1.3830F, 1.1919F, 1.0458F, 0.9220F, 0.8205F, 0.7305F, 0.6502F, 0.5950F, 0.5545F, 0.5333F, 0.5189F, 0.5181F, 0.5176F, 0.5172F, 0.5119F };
        float[] BValuesHelium = new float[COMPARTMENT_COUNT] { 0.4245F, 0.5747F, 0.6527F, 0.7223F, 0.7582F, 0.7957F, 0.8279F, 0.8553F, 0.8757F, 0.8903F, 0.8997F, 0.9073F, 0.9122F, 0.9171F, 0.9217F, 0.9267F };
        DiveModelProfile DiveModelProfile = new(COMPARTMENT_COUNT);
        DiveModelFactory diveModelFactory = new();

        // When
        DiveModel zhl16Buhlmann = diveModelFactory.CreateZhl16Buhlmann();

        // Then
        Assert.Equal(COMPARTMENT_COUNT, zhl16Buhlmann.CompartmentCount);
        Assert.Equal(Name, zhl16Buhlmann.Name);
        Assert.Equal(NitrogenHalfTime, zhl16Buhlmann.NitrogenHalfTime);
        Assert.Equal(HeliumHalfTime, zhl16Buhlmann.HeliumHalfTime);
        Assert.Equal(AValuesNitrogen, zhl16Buhlmann.AValuesNitrogen);
        Assert.Equal(BValuesNitrogen, zhl16Buhlmann.BValuesNitrogen);
        Assert.Equal(AValuesHelium, zhl16Buhlmann.AValuesHelium);
        Assert.Equal(BValuesHelium, zhl16Buhlmann.BValuesHelium);
        Assert.Equivalent(DiveModelProfile, zhl16Buhlmann.DiveModelProfile);
    }
}
