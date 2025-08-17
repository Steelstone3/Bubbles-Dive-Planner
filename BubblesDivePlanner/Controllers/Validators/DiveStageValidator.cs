public class DiveStageValidator : IValidator<DiveStage>
{
    public bool Validate(DiveStage diveStage)
    {
        DiveStepValidator diveStepValidator = new();
        CylinderValidator cylinderValidator = new();
        return diveStepValidator.Validate(diveStage.DiveStep) && cylinderValidator.Validate(diveStage.Cylinder);
    }
}
