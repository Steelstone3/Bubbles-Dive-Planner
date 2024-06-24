public class DiveStepPrototype : IDiveStepPrototype
{
    public IDiveStep DeepClone(IDiveStep diveStep)
    {
        IDiveStep newDiveStep = new DiveStep(new DiveStepValidator())
        {
            Depth = diveStep.Depth,
            Time = diveStep.Time,
        };

        return newDiveStep;
    }
}

public interface IDiveStepPrototype
{
    IDiveStep DeepClone(IDiveStep diveStep);
}