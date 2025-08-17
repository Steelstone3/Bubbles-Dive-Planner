public class CylinderValidator : IValidator<Cylinder>
{
    public bool IsValid(Cylinder cylinder)
    {
        bool isValidCylinder = IsValidCylinder(cylinder);

        if (!isValidCylinder)
        {
            return false;
        }

        GasMixtureValidator gasMixtureValidator = new();
        GasUsageValidator gasUsageValidator = new();
        return gasMixtureValidator.IsValid(cylinder.GasMixture) && gasUsageValidator.IsValid(cylinder.GasUsage);
    }

    private bool IsValidCylinder(Cylinder cylinder)
    {
        return !string.IsNullOrWhiteSpace(cylinder.Name) && cylinder.Pressure >= 50 && cylinder.Pressure <= 300 && cylinder.Volume >= 3 && cylinder.Volume <= 30;
    }
}
