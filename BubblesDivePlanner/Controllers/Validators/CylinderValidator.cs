public class CylinderValidator : ICylinderValidator
{
    public bool Validate(ICylinder cylinder)
    {
        bool isValid = !string.IsNullOrWhiteSpace(cylinder.Name) && cylinder.Pressure >= 50 && cylinder.Pressure <= 300 && cylinder.Volume >= 3 && cylinder.Volume <= 30;

        if (isValid)
        {
            return cylinder.GasMixture.IsValid && cylinder.GasUsage.IsValid; ;
        }

        return false;
    }
}

public interface ICylinderValidator
{
    bool Validate(ICylinder cylinder);
}