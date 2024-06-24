using ReactiveUI;

public class DivePlan : ReactiveObject, IDivePlan
{
    private IDiveModelSelector diveModelSelector = new DiveModelSelector();
    public IDiveModelSelector DiveModelSelector
    {
        get => diveModelSelector;
        set => this.RaiseAndSetIfChanged(ref diveModelSelector, value);
    }

    private ICylinderSelector cylinderSelector = new CylinderSelector();
    public ICylinderSelector CylinderSelector
    {
        get => cylinderSelector;
        set => this.RaiseAndSetIfChanged(ref cylinderSelector, value);
    }

    private IDiveStage diveStage = new DiveStage(new DiveStageValidator());
    public IDiveStage DiveStage
    {
        get => diveStage;
        set => this.RaiseAndSetIfChanged(ref diveStage, value);
    }

}

public interface IDivePlan
{
    public IDiveModelSelector DiveModelSelector
    {
        get;
        set;
    }

    public ICylinderSelector CylinderSelector
    {
        get;
        set;
    }

    public IDiveStage DiveStage
    {
        get;
        set;
    }
}