public class GasMixtureValidator : IGasMixtureValidator
{
    public float CalculateNitrogen(IGasMixture gasMixture)
    {
        return 100.0F - gasMixture.Oxygen - gasMixture.Helium;
    }

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
    float CalculateNitrogen(IGasMixture gasMixture);
    bool Validate(IGasMixture gasMixture);
}