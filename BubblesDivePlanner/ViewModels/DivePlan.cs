using ReactiveUI;

public class DivePlan : ReactiveObject, IDivePlan
{
    private IDiveModel diveModel;
    public IDiveModel DiveModel 
    {
        get => diveModel;
        set => this.RaiseAndSetIfChanged(ref diveModel, value);
    }

    private IDiveStep diveStep;
    public IDiveStep DiveStep 
    {
        get => diveStep;
        set => this.RaiseAndSetIfChanged(ref diveStep, value);
    }
}

public interface IDivePlan
{
    IDiveModel DiveModel { get; set; }
    IDiveStep DiveStep { get; set; }
}