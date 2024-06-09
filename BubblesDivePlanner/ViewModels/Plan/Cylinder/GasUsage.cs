using ReactiveUI;

public class GasUsage : ReactiveObject, IGasUsage
{
    private readonly IGasUsageValidator gasUsageValidator;

    public GasUsage(IGasUsageValidator gasUsageValidator)
    {
        this.gasUsageValidator = gasUsageValidator;
    }

    private ushort remaining;
    public ushort Remaining
    {
        get => remaining;
        set => this.RaiseAndSetIfChanged(ref remaining, value);
    }

    private ushort used;
    public ushort Used
    {
        get => used;
        set => this.RaiseAndSetIfChanged(ref used, value);
    }

    private byte surfaceAirConsumptionRate;
    public byte SurfaceAirConsumptionRate
    {
        get => surfaceAirConsumptionRate;
        set => this.RaiseAndSetIfChanged(ref surfaceAirConsumptionRate, value);
    }

    private bool isVisible;
    public bool IsVisible
    {
        get => isVisible;
        set => this.RaiseAndSetIfChanged(ref isVisible, value);
    }

    public bool IsValid => gasUsageValidator.Validate(this);
}

public interface IGasUsage : IVisibility, IValidation
{
    ushort Remaining { get; set; }
    ushort Used { get; set; }
    byte SurfaceAirConsumptionRate { get; set; }
}