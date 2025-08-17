public class DiveStagePrototype : IDiveStagePrototype
{
    private readonly IDiveModelPrototype diveModelPrototype;
    private readonly IDiveStepPrototype diveStepPrototype;

    public DiveStagePrototype(IDiveModelPrototype diveModelPrototype, IDiveStepPrototype diveStepPrototype)
    {
        this.diveModelPrototype = diveModelPrototype;
        this.diveStepPrototype = diveStepPrototype;
    }

    public DiveStage DeepClone(DiveStage diveStage)
    {
        return new DiveStage()
        {
            DiveModel = diveModelPrototype.DeepClone(diveStage.DiveModel),
            DiveStep = diveStepPrototype.DeepClone(diveStage.DiveStep),
            Cylinder = new Cylinder(diveStage.Cylinder),
        };
    }
}

public interface IDiveStagePrototype
{
    DiveStage DeepClone(DiveStage diveStage);
}