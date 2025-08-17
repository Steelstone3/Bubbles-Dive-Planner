public class CylinderPrototype : ICylinderPrototype
{
    public Cylinder DeepClone(Cylinder cylinder)
    {
        return new Cylinder()
        {
            Name = cylinder.Name,
            Volume = cylinder.Volume,
            Pressure = cylinder.Pressure,
            InitialPressurisedVolume = cylinder.InitialPressurisedVolume,
            GasMixture = new GasMixture()
            {
                Oxygen = cylinder.GasMixture.Oxygen,
                Helium = cylinder.GasMixture.Helium,
            },
            GasUsage = new GasUsage()
            {
                Remaining = cylinder.GasUsage.Remaining,
                Used = cylinder.GasUsage.Used,
                SurfaceAirConsumptionRate = cylinder.GasUsage.SurfaceAirConsumptionRate,
            }
        };
    }
}

public interface ICylinderPrototype
{
    Cylinder DeepClone(Cylinder cylinder);
}