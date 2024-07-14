public class MainPrototype : IMainPrototype
{
    public void NewInstance(IMain main)
    {
        DivePlan divePlan = new();
        main.DivePlan.DiveStage = divePlan.DiveStage;
        main.DivePlan.CylinderSelector.Cylinders.Clear();
        main.DivePlan.CylinderSelector.SetupCylinder = divePlan.CylinderSelector.SetupCylinder;
        main.DivePlan.CylinderSelector.SelectedCylinder = divePlan.CylinderSelector.SelectedCylinder;
        main.DivePlan.DiveModelSelector.IsVisible = true;

        DiveInformation diveInformation = new();
        main.DiveInformation.DecompressionProfile.DecompressionSteps.Clear();
        main.DiveInformation.DecompressionProfile.DiveCeiling = diveInformation.DecompressionProfile.DiveCeiling;

        main.Result.Results.Clear();
    }
}

public interface IMainPrototype
{
    void NewInstance(IMain main);
}