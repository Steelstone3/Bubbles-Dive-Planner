public class DiveStagePrototype : IDiveStagePrototype
{
    private readonly IDiveModelPrototype diveModelPrototype;
    private readonly IDiveStepPrototype diveStepPrototype;
    private readonly ICylinderPrototype cylinderPrototype;

    public DiveStagePrototype(IDiveModelPrototype diveModelPrototype, IDiveStepPrototype diveStepPrototype, ICylinderPrototype cylinderPrototype)
    {
        this.diveModelPrototype = diveModelPrototype;
        this.diveStepPrototype = diveStepPrototype;
        this.cylinderPrototype = cylinderPrototype;
    }

    public DiveStage DeepClone(DiveStage diveStage)
    {
        return new DiveStage()
        {
            DiveModel = diveModelPrototype.DeepClone(diveStage.DiveModel),
            DiveStep = diveStepPrototype.DeepClone(diveStage.DiveStep),
            Cylinder = cylinderPrototype.DeepClone(diveStage.Cylinder),
        };
    }
}

public interface IDiveStagePrototype
{
    DiveStage DeepClone(DiveStage diveStage);
}