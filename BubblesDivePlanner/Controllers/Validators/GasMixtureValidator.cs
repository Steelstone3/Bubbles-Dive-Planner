public class GasMixtureValidator : IValidator<GasMixture>
{
    public bool IsValid(GasMixture gasMixture)
    {
        if (gasMixture.Oxygen < 5.0F || gasMixture.Oxygen + gasMixture.Helium + gasMixture.Nitrogen > 100.0F)
        {
            return false;
        }

        return true;
    }
}