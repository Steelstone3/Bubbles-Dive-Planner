// TODO AH Test
using DynamicData;

public class DalCylinderSelectorConverter : IDalConverter<DalCylinderSelector, ICylinderSelector>
{
    public DalCylinderSelector ConvertTo(ICylinderSelector cylinderSelector)
    {
        DalCylinderConverter dalCylinderConverter = new();

        DalCylinderSelector dalCylinderSelector = new()
        {
            SetupCylinder = dalCylinderConverter.ConvertTo(cylinderSelector.SetupCylinder),
            SelectedCylinder = dalCylinderConverter.ConvertTo(cylinderSelector.SelectedCylinder),
        };

        List<DalCylinder> cylinders = [];
        foreach (ICylinder cylinder in cylinderSelector.Cylinders)
        {
            cylinders.Add(dalCylinderConverter.ConvertTo(cylinder));
        }
        dalCylinderSelector.Cylinders = [.. cylinders];

        return dalCylinderSelector;
    }

    public ICylinderSelector ConvertFrom(DalCylinderSelector dalCoverterType)
    {
        return null;
    }
}