public class DalGasMixtureConverter : IDalConverter<DalGasMixture, IGasMixture>
{
    public IGasMixture ConvertFrom(DalGasMixture dalGasMixture)
    {
        throw new NotImplementedException();
    }

    public DalGasMixture ConvertTo(IGasMixture gasMixture)
    {
        return new()
        {
            Oxygen = gasMixture.Oxygen,
            Helium = gasMixture.Helium,
        };
    }
}
