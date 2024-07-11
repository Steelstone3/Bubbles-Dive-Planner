public class DalGasUsageConverter : IDalConverter<DalGasUsage, IGasUsage>
{
    public IGasUsage ConvertFrom(DalGasUsage dalGasUsage)
    {
        throw new NotImplementedException();
    }

    public DalGasUsage ConvertTo(IGasUsage gasUsage)
    {
        return new()
        {
            Remaining = gasUsage.Remaining,
            Used = gasUsage.Used,
            SurfaceAirConsumptionRate = gasUsage.SurfaceAirConsumptionRate,
        };
    }
}