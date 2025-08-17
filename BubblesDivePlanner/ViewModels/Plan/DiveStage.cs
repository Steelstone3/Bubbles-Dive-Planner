using ReactiveUI;

public class DiveStage : ReactiveObject
{
    public DiveStage()
    {

    }

    private DiveModel diveModel;
    public DiveModel DiveModel
    {
        get => diveModel;
        set => this.RaiseAndSetIfChanged(ref diveModel, value);
    }

    private DiveStep diveStep = new();
    public DiveStep DiveStep
    {
        get => diveStep;
        set => this.RaiseAndSetIfChanged(ref diveStep, value);
    }

    private Cylinder cylinder = new();
    public Cylinder Cylinder
    {
        get => cylinder;
        set => this.RaiseAndSetIfChanged(ref cylinder, value);
    }
}
