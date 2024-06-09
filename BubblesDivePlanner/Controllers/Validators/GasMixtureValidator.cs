public class GasMixtureValidator : IGasMixtureValidator
{
    public bool Validate(IGasMixture gasMixture)
    {
        if (gasMixture.Oxygen < 5.0F || gasMixture.Oxygen + gasMixture.Helium + gasMixture.Nitrogen > 100.0F)
        {
            return false;
        }

        return true;
    }
}

public interface IGasMixtureValidator
{
    bool Validate(IGasMixture gasMixture);
}