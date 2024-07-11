// TODO AH Test
public class DalGasUsageConverter : IDalConverter<DalGasUsage, IGasUsage>
{
    public DalGasUsage ConvertTo(IGasUsage gasUsage)
    {
        return new()
        {
            Remaining = gasUsage.Remaining,
            Used = gasUsage.Used,
            SurfaceAirConsumptionRate = gasUsage.SurfaceAirConsumptionRate,
        };
    }

    public IGasUsage ConvertFrom(DalGasUsage dalGasUsage)
    {
        return new GasUsage(new GasUsageValidator())
        {
            Remaining = dalGasUsage.Remaining,
            Used = dalGasUsage.Used,
            SurfaceAirConsumptionRate = dalGasUsage.SurfaceAirConsumptionRate,
        };
    }
}