public class DiveStepPrototype : IDiveStepPrototype
{
    public DiveStep DeepClone(DiveStep diveStep)
    {
        return new DiveStep()
        {
            Depth = diveStep.Depth,
            Time = diveStep.Time,
        };
    }
}

public interface IDiveStepPrototype
{
    DiveStep DeepClone(DiveStep diveStep);
}