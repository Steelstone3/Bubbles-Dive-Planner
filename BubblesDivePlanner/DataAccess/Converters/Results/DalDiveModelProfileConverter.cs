// TODO AH Test
public class DalDiveModelProfileConverter : IDalConverter<DalDiveModelProfile, IDiveModelProfile>
{
    public IDiveModelProfile ConvertFrom(DalDiveModelProfile dalDiveModelProfile)
    {
        return new DiveModelProfile((byte)(dalDiveModelProfile.CompartmentLoads.Length - 1), new DiveBoundaryController())
        {
            OxygenAtPressure = dalDiveModelProfile.OxygenAtPressure,
            NitrogenAtPressure = dalDiveModelProfile.NitrogenAtPressure,
            HeliumAtPressure = dalDiveModelProfile.HeliumAtPressure,
            NitrogenTissuePressures = dalDiveModelProfile.NitrogenTissuePressures,
            HeliumTissuePressures = dalDiveModelProfile.HeliumTissuePressures,
            TotalTissuePressures = dalDiveModelProfile.TotalTissuePressures,
            AValues = dalDiveModelProfile.AValues,
            BValues = dalDiveModelProfile.BValues,
            ToleratedAmbientPressures = dalDiveModelProfile.ToleratedAmbientPressures,
            MaxSurfacePressures = dalDiveModelProfile.MaxSurfacePressures,
            CompartmentLoads = dalDiveModelProfile.CompartmentLoads
        };
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