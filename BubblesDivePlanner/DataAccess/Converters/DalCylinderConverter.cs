public class DalCylinderConverter : IDalConverter<DalCylinder, ICylinder>
{
    public ICylinder ConvertFrom(DalCylinder dalCylinder)
    {
        throw new NotImplementedException();
    }

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
}