public class CylinderController : ICylinderController
{
    public ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure) => (ushort)(volume * pressure);

    // public ushort CalculateRemainingPressurisedVolume(ushort gasRemaining, ushort gasUsed)
    // {
    //     return gasRemaining > gasUsed ? (ushort)(gasRemaining - gasUsed) : (ushort)0;
    // }

    // public ushort CalculateGasUsed(IDiveStep diveStep, byte surfaceAirConsumptionRate)
    // {
    //     return (ushort)(((diveStep.Depth / 10) + 1) * diveStep.Time * surfaceAirConsumptionRate);
    // }

    public float CalculateNitrogen(float oxygen, float helium) => 100.0F - oxygen - helium;
}

public interface ICylinderController
{
    ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure);
    float CalculateNitrogen(float oxygen, float helium);
}