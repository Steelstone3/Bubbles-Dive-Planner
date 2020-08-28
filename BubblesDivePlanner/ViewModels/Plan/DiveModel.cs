using ReactiveUI;

public class DiveModel : ReactiveObject, IDiveModel
{
    public string Name { get; set; }
    public byte CompartmentCount { get; set; }
    public float[] NitrogenHalfTime { get; set; }
    public float[] HeliumHalfTime { get; set; }
    public float[] AValuesNitrogen { get; set; }
    public float[] BValuesNitrogen { get; set; }
    public float[] AValuesHelium { get; set; }
    public float[] BValuesHelium { get; set; }

    private IDiveModelProfile diveModelProfile;
    public IDiveModelProfile DiveModelProfile
    {
        get => diveModelProfile;
        set => this.RaiseAndSetIfChanged(ref diveModelProfile, value);
    }
}

public interface IDiveModel
{
    string Name { get; set; }
    byte CompartmentCount { get; set; }
    float[] NitrogenHalfTime { get; set; }
    float[] HeliumHalfTime { get; set; }
    float[] AValuesNitrogen { get; set; }
    float[] BValuesNitrogen { get; set; }
    float[] AValuesHelium { get; set; }
    float[] BValuesHelium { get; set; }
    IDiveModelProfile DiveModelProfile { get; set; }
}
