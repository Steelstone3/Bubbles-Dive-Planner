// TODO AH Test
public class DalGasUsageConverter : IDalConverter<DalGasUsage, GasUsage>
{
    public DalGasUsage ConvertTo(GasUsage gasUsage)
    {
        return new()
        {
            Remaining = gasUsage.Remaining,
            Used = gasUsage.Used,
            SurfaceAirConsumptionRate = gasUsage.SurfaceAirConsumptionRate,
        };
    }

    public GasUsage ConvertFrom(DalGasUsage dalGasUsage)
    {
        return new GasUsage()
        {
            Remaining = dalGasUsage.Remaining,
            Used = dalGasUsage.Used,
            SurfaceAirConsumptionRate = dalGasUsage.SurfaceAirConsumptionRate,
        };
    }
}