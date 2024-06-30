public class CylinderPrototype : ICylinderPrototype
{
    public ICylinder DeepClone(ICylinder cylinder)
    {
        return new Cylinder(new CylinderValidator(), new CylinderController())
        {
            Name = cylinder.Name,
            Volume = cylinder.Volume,
            Pressure = cylinder.Pressure,
            InitialPressurisedVolume = cylinder.InitialPressurisedVolume,
            GasMixture = new GasMixture(new GasMixtureValidator(), new CylinderController(), new DiveBoundaryController())
            {
                Oxygen = cylinder.GasMixture.Oxygen,
                Helium = cylinder.GasMixture.Helium,
            },
            GasUsage = new GasUsage(new GasUsageValidator())
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
    ICylinder DeepClone(ICylinder cylinder);
}