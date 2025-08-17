public class DiveStagePrototype : IDiveStagePrototype
{
    private readonly IDiveModelPrototype diveModelPrototype;

    public DiveStagePrototype(IDiveModelPrototype diveModelPrototype)
    {
        this.diveModelPrototype = diveModelPrototype;
    }

    public DiveStage DeepClone(DiveStage diveStage)
    {
        return new DiveStage()
        {
            DiveModel = diveModelPrototype.DeepClone(diveStage.DiveModel),
            DiveStep = new DiveStep(diveStage.DiveStep),
            Cylinder = new Cylinder(diveStage.Cylinder),
        };
    }
}

public interface IDiveStagePrototype
{
    DiveStage DeepClone(DiveStage diveStage);
}