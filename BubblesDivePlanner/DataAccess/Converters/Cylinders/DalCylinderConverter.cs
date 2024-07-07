// TODO AH Test
public class DalCylinderConverter : IDalConverter<DalCylinder, ICylinder>
{
    public DalCylinder ConvertTo(ICylinder cylinder)
    {
        DalGasMixtureConverter dalGasMixtureConverter = new();
        DalGasUsageConverter dalGasUsageConverter = new();

        return new()
        {
            Name = cylinder.Name,
            Volume = cylinder.Volume,
            Pressure = cylinder.Pressure,
            InitialPressurisedVolume = cylinder.InitialPressurisedVolume,
            GasMixture = dalGasMixtureConverter.ConvertTo(cylinder.GasMixture),
            GasUsage = dalGasUsageConverter.ConvertTo(cylinder.GasUsage)
        };
    }

    public ICylinder ConvertFrom(DalCylinder dalCylinder)
    {
        DalGasMixtureConverter dalGasMixtureConverter = new();
        DalGasUsageConverter dalGasUsageConverter = new();

        return new Cylinder(new CylinderValidator(), new CylinderController())
        {
            Name = dalCylinder.Name,
            Volume = dalCylinder.Volume,
            Pressure = dalCylinder.Pressure,
            InitialPressurisedVolume = dalCylinder.InitialPressurisedVolume,
            GasMixture = dalGasMixtureConverter.ConvertFrom(dalCylinder.GasMixture),
            GasUsage = dalGasUsageConverter.ConvertFrom(dalCylinder.GasUsage)
        };
    }
}