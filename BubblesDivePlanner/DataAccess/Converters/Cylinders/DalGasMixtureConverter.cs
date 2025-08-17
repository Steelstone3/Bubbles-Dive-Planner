// TODO AH Test
public class DalGasMixtureConverter : IDalConverter<DalGasMixture, GasMixture>
{
    public DalGasMixture ConvertTo(GasMixture gasMixture)
    {
        return new()
        {
            Oxygen = gasMixture.Oxygen,
            Helium = gasMixture.Helium,
        };
    }

    public GasMixture ConvertFrom(DalGasMixture dalGasMixture)
    {
        return new GasMixture()
        {
            Oxygen = dalGasMixture.Oxygen,
            Helium = dalGasMixture.Helium,
        };
    }
}
