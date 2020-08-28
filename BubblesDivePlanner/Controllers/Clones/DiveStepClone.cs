public class DiveStepClone
{
    public IDiveStep Clone(IDiveStep diveStep)
    {
        return new DiveStep
        {
            Depth = diveStep.Depth,
            Time = diveStep.Time
        };
    }
}