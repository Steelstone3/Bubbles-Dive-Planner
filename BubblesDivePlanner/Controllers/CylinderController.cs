public class CylinderController : ICylinderController
{
    public ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure) => (ushort)(volume * pressure);

    public IGasUsage UpdateGasUsage(IDiveStep diveStep, IGasUsage gasUsage)
    {
        gasUsage.Used = CalculateGasUsed(diveStep, gasUsage);
        gasUsage.Remaining = CalculateRemainingPressurisedVolume(gasUsage);

        return gasUsage;
    }

    public float CalculateNitrogen(float oxygen, float helium) => 100.0F - oxygen - helium;

    private ushort CalculateRemainingPressurisedVolume(IGasUsage gasUsage) => gasUsage.Remaining > gasUsage.Used ? (ushort)(gasUsage.Remaining - gasUsage.Used) : (ushort)0;

    private ushort CalculateGasUsed(IDiveStep diveStep, IGasUsage gasUsage) => (ushort)(((diveStep.Depth / 10) + 1) * diveStep.Time * gasUsage.SurfaceAirConsumptionRate);

}

public interface ICylinderController
{
    ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure);
    IGasUsage UpdateGasUsage(IDiveStep diveStep, IGasUsage gasUsage);
    float CalculateNitrogen(float oxygen, float helium);
}