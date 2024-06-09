public class DiveStagePrototype : IDiveStagePrototype
{
    private readonly IDiveStepPrototype diveStepPrototype;
    private readonly ICylinderPrototype cylinderPrototype;

    public DiveStagePrototype(IDiveStepPrototype diveStepPrototype, ICylinderPrototype cylinderPrototype)
    {
        this.diveStepPrototype = diveStepPrototype;
        this.cylinderPrototype = cylinderPrototype;
    }

    public IDiveStage DeepClone(IDiveStage diveStage)
    {
        IDiveStage clonedDiveStage = new DiveStage(new DiveStageValidator())
        {
            DiveStep = diveStepPrototype.DeepClone(diveStage.DiveStep),
            Cylinder = cylinderPrototype.DeepClone(diveStage.Cylinder),
            DiveModel = diveStage.DiveModel,
        };

        return clonedDiveStage;
    }
}

public interface IDiveStagePrototype
{
    IDiveStage DeepClone(IDiveStage diveStage);
}