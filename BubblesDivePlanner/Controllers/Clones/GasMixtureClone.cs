public class GasMixtureClone
{
    public IGasMixture Clone(IGasMixture gasMixture)
    {
        return new GasMixture()
        {
            Oxygen = gasMixture.Oxygen,
            Helium = gasMixture.Helium,
        };
    }
}