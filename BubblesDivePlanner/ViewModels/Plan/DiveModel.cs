using ReactiveUI;

public class DiveModel : ReactiveObject
{
    public string Name { get; set; }
    public byte CompartmentCount { get; set; }
    public float[] NitrogenHalfTime { get; set; }
    public float[] HeliumHalfTime { get; set; }
    public float[] AValuesNitrogen { get; set; }
    public float[] BValuesNitrogen { get; set; }
    public float[] AValuesHelium { get; set; }
    public float[] BValuesHelium { get; set; }

    private DiveModelProfile diveModelProfile;
    public DiveModelProfile DiveModelProfile
    {
        get => diveModelProfile;
        set => this.RaiseAndSetIfChanged(ref diveModelProfile, value);
    }
}