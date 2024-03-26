public class GasMixtureValidator : IGasMixtureValidator
{
    public float CalculateNitrogen(IGasMixture gasMixture)
    {
        return 100.0F - gasMixture.Oxygen - gasMixture.Helium;
    }
}

public interface IGasMixtureValidator
{
    float CalculateNitrogen(IGasMixture gasMixture);
}