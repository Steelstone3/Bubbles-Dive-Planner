public class GasUsageValidator : IValidator<GasUsage>
{
    public bool IsValid(GasUsage gasUsage)
    {
        return gasUsage.SurfaceAirConsumptionRate <= 30 && gasUsage.SurfaceAirConsumptionRate >= 6;
    }
}
