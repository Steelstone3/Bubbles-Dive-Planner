public class MainPrototype : IMainPrototype
{
    public void NewInstance(IMain main)
    {
        DivePlan divePlan = new();
        main.DivePlan.DiveStage = divePlan.DiveStage;
        main.DivePlan.CylinderSelector = divePlan.CylinderSelector;
        main.DivePlan.DiveModelSelector.IsVisible = true;

        main.DiveInformation = new DiveInformation();
        main.Result = new Result();
    }
}

public interface IMainPrototype
{
    void NewInstance(IMain main);
}