// TODO AH Test
public class DalDiveModelProfileConverter : IDalConverter<DalDiveModelProfile, IDiveModelProfile>
{
    public IDiveModelProfile ConvertFrom(DalDiveModelProfile dalDiveModelProfile)
    {
        throw new NotImplementedException();
    }

    public DalDiveModelProfile ConvertTo(IDiveModelProfile diveModelProfile)
    {
        return new()
        {
            OxygenAtPressure = diveModelProfile.OxygenAtPressure,
            NitrogenAtPressure = diveModelProfile.NitrogenAtPressure,
            HeliumAtPressure = diveModelProfile.HeliumAtPressure,
            NitrogenTissuePressures = diveModelProfile.NitrogenTissuePressures,
            HeliumTissuePressures = diveModelProfile.HeliumTissuePressures,
            TotalTissuePressures = diveModelProfile.TotalTissuePressures,
            AValues = diveModelProfile.AValues,
            BValues = diveModelProfile.BValues,
            ToleratedAmbientPressures = diveModelProfile.ToleratedAmbientPressures,
            MaxSurfacePressures = diveModelProfile.MaxSurfacePressures,
            CompartmentLoads = diveModelProfile.CompartmentLoads
        };
    }
}