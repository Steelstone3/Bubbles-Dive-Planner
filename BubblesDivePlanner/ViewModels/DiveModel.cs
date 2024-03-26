using ReactiveUI;

public class DiveModel : ReactiveObject, IDiveModel
{
    public string Name { get; protected set; }
    public byte CompartmentCount { get; protected set; }
    public float[] NitrogenHalfTime { get; protected set; }
    public float[] HeliumHalfTime { get; protected set; }
    public float[] AValuesNitrogen { get; protected set; }
    public float[] BValuesNitrogen { get; protected set; }
    public float[] AValuesHelium { get; protected set; }
    public float[] BValuesHelium { get; protected set; }

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
