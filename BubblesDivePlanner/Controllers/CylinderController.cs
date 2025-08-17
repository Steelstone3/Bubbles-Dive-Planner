public class CylinderController : ICylinderController
{
    public ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure) => (ushort)(volume * pressure);

    public GasUsage UpdateGasUsage(DiveStep diveStep, GasUsage gasUsage)
    {
        gasUsage.Used = CalculateGasUsed(diveStep, gasUsage);
        gasUsage.Remaining = CalculateRemainingPressurisedVolume(gasUsage);

        return gasUsage;
    }

    public float CalculateNitrogen(float oxygen, float helium) => 100.0F - oxygen - helium;

    private ushort CalculateRemainingPressurisedVolume(GasUsage gasUsage) => gasUsage.Remaining > gasUsage.Used ? (ushort)(gasUsage.Remaining - gasUsage.Used) : (ushort)0;

    private ushort CalculateGasUsed(DiveStep diveStep, GasUsage gasUsage) => (ushort)(((diveStep.Depth / 10) + 1) * diveStep.Time * gasUsage.SurfaceAirConsumptionRate);

}

public interface ICylinderController
{
    ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure);
    GasUsage UpdateGasUsage(DiveStep diveStep, GasUsage gasUsage);
    float CalculateNitrogen(float oxygen, float helium);
}