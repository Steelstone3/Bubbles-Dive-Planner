using ReactiveUI;

public class DivePlan : ReactiveObject
{
    private DiveModelSelector diveModelSelector = new DiveModelSelector();
    public DiveModelSelector DiveModelSelector
    {
        get => diveModelSelector;
        set => this.RaiseAndSetIfChanged(ref diveModelSelector, value);
    }

    private CylinderSelector cylinderSelector = new CylinderSelector();
    public CylinderSelector CylinderSelector
    {
        get => cylinderSelector;
        set => this.RaiseAndSetIfChanged(ref cylinderSelector, value);
    }

    private DiveStage diveStage = new();
    public DiveStage DiveStage
    {
        get => diveStage;
        set => this.RaiseAndSetIfChanged(ref diveStage, value);
    }
}
