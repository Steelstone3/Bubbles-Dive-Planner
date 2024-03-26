using ReactiveUI;

public class DiveModel : ReactiveObject, IDiveModel
{
    public string Name { get; }
    public byte CompartmentCount { get; }
    public float[] NitrogenHalfTime { get; }
    public float[] HeliumHalfTime { get; }
    public float[] AValuesNitrogen { get; }
    public float[] BValuesNitrogen { get; }
    public float[] AValuesHelium { get; }
    public float[] BValuesHelium { get; }

    private IDiveModelProfile diveModelProfile;
    public IDiveModelProfile DiveModelProfile
    {
        get => diveModelProfile;
        set => this.RaiseAndSetIfChanged(ref diveModelProfile, value);
    }
}

public interface IDiveModel
{
    string Name { get; }
    byte CompartmentCount { get; }
    float[] NitrogenHalfTime { get; }
    float[] HeliumHalfTime { get; }
    float[] AValuesNitrogen { get; }
    float[] BValuesNitrogen { get; }
    float[] AValuesHelium { get; }
    float[] BValuesHelium { get; }
    IDiveModelProfile DiveModelProfile { get; set; }
}
