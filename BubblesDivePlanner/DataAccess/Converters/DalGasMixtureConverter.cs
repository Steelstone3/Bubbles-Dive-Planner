// TODO AH Test
public class DalGasMixtureConverter : IDalConverter<DalGasMixture, IGasMixture>
{
    public DalGasMixture ConvertTo(IGasMixture gasMixture)
    {
        return new()
        {
            Oxygen = gasMixture.Oxygen,
            Helium = gasMixture.Helium,
        };
    }

    public IGasMixture ConvertFrom(DalGasMixture dalGasMixture)
    {
        return new GasMixture(new GasMixtureValidator(), new CylinderController(), new DiveBoundaryController())
        {
            Oxygen = dalGasMixture.Oxygen,
            Helium = dalGasMixture.Helium,
        };
    }
}
