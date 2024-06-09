public class GasUsageValidator : IGasUsageValidator
{
    public bool Validate(IGasUsage gasUsage)
    {
        return gasUsage.SurfaceAirConsumptionRate <= 30 && gasUsage.SurfaceAirConsumptionRate >= 6;
    }
}

public interface IGasUsageValidator
{
    bool Validate(IGasUsage gasUsage);
}