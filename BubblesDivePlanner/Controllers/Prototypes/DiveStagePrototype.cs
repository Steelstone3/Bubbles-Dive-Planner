public class DiveStagePrototype : IDiveStagePrototype
{
    public DiveStage DeepClone(DiveStage diveStage)
    {
        return new DiveStage()
        {
            DiveModel = new DiveModel(diveStage.DiveModel),
            DiveStep = new DiveStep(diveStage.DiveStep),
            Cylinder = new Cylinder(diveStage.Cylinder),
        };
    }
}

public interface IDiveStagePrototype
{
    DiveStage DeepClone(DiveStage diveStage);
}