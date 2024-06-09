public class CylinderPrototype : ICylinderPrototype
{
    public ICylinder DeepClone(ICylinder cylinder)
    {
        ICylinder newCylinder = new Cylinder(new CylinderValidator(), new CylinderController())
        {
            Name = cylinder.Name,
            Volume = cylinder.Volume,
            Pressure = cylinder.Pressure,
            InitialPressurisedVolume = cylinder.InitialPressurisedVolume,
            GasMixture = new GasMixture(new GasMixtureValidator(), new CylinderController())
            {
                Oxygen = cylinder.GasMixture.Oxygen,
                Helium = cylinder.GasMixture.Helium,
                IsVisible = cylinder.GasMixture.IsVisible,
            },
            GasUsage = new GasUsage(new GasUsageValidator())
            {
                Remaining = cylinder.GasUsage.Remaining,
                Used = cylinder.GasUsage.Used,
                SurfaceAirConsumptionRate = cylinder.GasUsage.SurfaceAirConsumptionRate,
                IsVisible = cylinder.GasUsage.IsVisible,
            }
        };

        return newCylinder;
    }
}

public interface ICylinderPrototype
{
    ICylinder DeepClone(ICylinder cylinder);
}