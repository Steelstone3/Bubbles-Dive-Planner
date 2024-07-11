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

        main.DiveInformation = new DiveInformation();
        main.Result = new Result();
    }
}

public interface IMainPrototype
{
    void NewInstance(IMain main);
}