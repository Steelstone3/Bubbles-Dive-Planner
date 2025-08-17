using ReactiveUI;

public class DiveModel : ReactiveObject
{
    public DiveModel(DiveModel diveModel)
    {
        Name = diveModel.Name;
        CompartmentCount = diveModel.CompartmentCount;
        NitrogenHalfTime = diveModel.NitrogenHalfTime;
        HeliumHalfTime = diveModel.HeliumHalfTime;
        AValuesNitrogen = diveModel.AValuesNitrogen;
        BValuesNitrogen = diveModel.BValuesNitrogen;
        AValuesHelium = diveModel.AValuesHelium;
        BValuesHelium = diveModel.BValuesHelium;
        DiveModelProfile = new DiveModelProfile(diveModel.DiveModelProfile);
    }

    public DiveModel() { }

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