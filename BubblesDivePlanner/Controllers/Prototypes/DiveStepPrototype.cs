public class DiveStepPrototype : IDiveStepPrototype
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
            },
            GasUsage = new GasUsage(new GasUsageValidator())
            {
                Remaining = cylinder.GasUsage.Remaining,
                Used = cylinder.GasUsage.Used,
                SurfaceAirConsumptionRate = cylinder.GasUsage.SurfaceAirConsumptionRate,
            }
        };

        return newCylinder;
    }

    public IDiveStep DeepClone(IDiveStep diveStep)
    {
        IDiveStep newDiveStep = new DiveStep(new DiveStepValidator())
        {
            Depth = diveStep.Depth,
            Time = diveStep.Time,
        };

        return newDiveStep;
    }
}

public interface IDiveStepPrototype
{
    IDiveStep DeepClone(IDiveStep diveStep);
}