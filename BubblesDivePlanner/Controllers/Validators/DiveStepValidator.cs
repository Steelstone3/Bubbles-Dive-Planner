public class DiveStepValidator : IDiveStepValidator
{
    public bool Validate(IDiveStep diveStep)
    {
        if (diveStep.Depth < 1 || diveStep.Depth > 101 || diveStep.Time < 1 || diveStep.Time > 60)
        {
            return false;
        }

        return true;
    }
}

public interface IDiveStepValidator
{
    bool Validate(IDiveStep diveStep);
}