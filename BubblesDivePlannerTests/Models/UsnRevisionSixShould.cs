using Xunit;

public class UsnRevisionSixShould
{
    [Fact]
    public void Construct()
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
        UsnRevisionSix usnRevisionSix = new();

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
}