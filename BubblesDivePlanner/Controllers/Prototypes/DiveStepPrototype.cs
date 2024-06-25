public class DiveStepPrototype : IDiveStepPrototype
{
    public IDiveStep DeepClone(IDiveStep diveStep)
    {
        return new DiveStep(new DiveStepValidator())
        {
            Depth = diveStep.Depth,
            Time = diveStep.Time,
        };
    }
}

public interface IDiveStepPrototype
{
    IDiveStep DeepClone(IDiveStep diveStep);
}