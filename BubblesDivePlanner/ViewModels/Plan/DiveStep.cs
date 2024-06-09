using ReactiveUI;

public class DiveStep : ReactiveObject, IDiveStep
{
    private readonly IDiveStepValidator diveStepValidator;

    public DiveStep(IDiveStepValidator diveStepValidator)
    {
        this.diveStepValidator = diveStepValidator;
    }

    private byte _depth;
    public byte Depth
    {
        get => _depth;
        set => this.RaiseAndSetIfChanged(ref _depth, value);
    }

    private byte _time;
    public byte Time
    {
        get => _time;
        set => this.RaiseAndSetIfChanged(ref _time, value);
    }

    public bool IsValid => diveStepValidator.Validate(this);
}

public interface IDiveStep : IValidation
{
    byte Depth { get; set; }
    byte Time { get; set; }
}