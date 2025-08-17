public class DiveStepValidator : IValidator<DiveStep>
{
    public bool Validate(DiveStep diveStep)
    {
        if (diveStep.Depth < 1 || diveStep.Depth > 101 || diveStep.Time < 1 || diveStep.Time > 60)
        {
            return false;
        }

        return true;
    }
}
