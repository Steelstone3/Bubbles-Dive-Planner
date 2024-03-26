using ReactiveUI;

public class DiveStep : ReactiveObject, IDiveStep
{
    private readonly IDiveStepValidator diveStepValidator = new DiveStepValidator();

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

    // TODO AH Test
    public bool IsValid => diveStepValidator.Validate(this);
}

public interface IDiveStep : IValidation
{
    byte Depth { get; set; }
    byte Time { get; set; }
}