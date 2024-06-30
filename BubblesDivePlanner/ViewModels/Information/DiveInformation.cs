using ReactiveUI;

public class DiveInformation : ReactiveObject, IDiveInformation
{
    private float diveCeiling;
    public float DiveCeiling
    {
        get => diveCeiling;
        set => this.RaiseAndSetIfChanged(ref diveCeiling, value);
    }
}

public interface IDiveInformation
{
    float DiveCeiling { get; set; }
}