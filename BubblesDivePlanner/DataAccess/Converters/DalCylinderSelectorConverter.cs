// TODO AH Test
using DynamicData;

public class DalCylinderSelectorConverter : IDalConverter<DalCylinderSelector, ICylinderSelector>
{
    public DalCylinderSelector ConvertTo(ICylinderSelector cylinderSelector)
    {
        DalCylinderSelector dalCylinderSelector = new()
        {
            SetupCylinder = CylinderConvertTo(cylinderSelector.SetupCylinder),
            SelectedCylinder = CylinderConvertTo(cylinderSelector.SelectedCylinder),
        };

        // Add DalCylinders
        List<DalCylinder> cylinders = [];
        foreach (ICylinder item in cylinderSelector.Cylinders)
        {
            cylinders.Add(CylinderConvertTo(item));
        }
        dalCylinderSelector.Cylinders = [.. cylinders];

        return dalCylinderSelector;
    }

    public ICylinderSelector ConvertFrom(DalCylinderSelector dalCoverterType)
    {
        return null;
    }

    private DalCylinder CylinderConvertTo(ICylinder cylinder)
    {
        return new()
        {
            Name = cylinder.Name,
            Volume = cylinder.Volume,
            Pressure = cylinder.Pressure,
            InitialPressurisedVolume = cylinder.InitialPressurisedVolume,
            GasMixture = GasMixtureConvertTo(cylinder.GasMixture),
            GasUsage = GasUsageConvertTo(cylinder.GasUsage)
        };
    }

    private DalGasMixture GasMixtureConvertTo(IGasMixture gasMixture)
    {
        return new()
        {
            Oxygen = gasMixture.Oxygen,
            Helium = gasMixture.Helium,
        };
    }

    private DalGasUsage GasUsageConvertTo(IGasUsage gasUsage)
    {
        return new()
        {
            Remaining = gasUsage.Remaining,
            Used = gasUsage.Used,
            SurfaceAirConsumptionRate = gasUsage.SurfaceAirConsumptionRate,
        };
    }
}