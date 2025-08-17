// TODO AH Test
public class DalCylinderSelectorConverter : IDalConverter<DalCylinderSelector, CylinderSelector>
{
    public DalCylinderSelector ConvertTo(CylinderSelector cylinderSelector)
    {
        DalCylinderConverter dalCylinderConverter = new();

        DalCylinderSelector dalCylinderSelector = new()
        {
            SetupCylinder = dalCylinderConverter.ConvertTo(cylinderSelector.SetupCylinder),
            SelectedCylinder = dalCylinderConverter.ConvertTo(cylinderSelector.SelectedCylinder),
        };

        List<DalCylinder> cylinders = [];
        foreach (Cylinder cylinder in cylinderSelector.Cylinders)
        {
            cylinders.Add(dalCylinderConverter.ConvertTo(cylinder));
        }
        dalCylinderSelector.Cylinders = [.. cylinders];

        return dalCylinderSelector;
    }

    public CylinderSelector ConvertFrom(DalCylinderSelector dalCylinderSelector)
    {
        DalCylinderConverter dalCylinderConverter = new();

        CylinderSelector cylinderSelector = new()
        {
            SetupCylinder = dalCylinderConverter.ConvertFrom(dalCylinderSelector.SetupCylinder),
            SelectedCylinder = dalCylinderConverter.ConvertFrom(dalCylinderSelector.SelectedCylinder),
        };

        foreach (DalCylinder dalCylinder in dalCylinderSelector.Cylinders)
        {
            cylinderSelector.Cylinders.Add(dalCylinderConverter.ConvertFrom(dalCylinder));
        }

        return cylinderSelector;
    }
}