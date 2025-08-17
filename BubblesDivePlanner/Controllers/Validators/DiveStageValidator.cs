public class DiveStageValidator : IValidator<DiveStage>
{
    public bool IsValid(DiveStage diveStage)
    {
        DiveStepValidator diveStepValidator = new();
        CylinderValidator cylinderValidator = new();
        return diveStepValidator.IsValid(diveStage.DiveStep) && cylinderValidator.IsValid(diveStage.Cylinder);
    }
}
