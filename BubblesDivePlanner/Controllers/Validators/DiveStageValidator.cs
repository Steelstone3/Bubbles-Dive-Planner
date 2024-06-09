public class DiveStageValidator : IDiveStageValidator
{
    public bool Validate(IDiveStage diveStage)
    {
        return diveStage.DiveStep.IsValid && diveStage.Cylinder.IsValid;
    }
}

public interface IDiveStageValidator
{
    bool Validate(IDiveStage diveStage);
}