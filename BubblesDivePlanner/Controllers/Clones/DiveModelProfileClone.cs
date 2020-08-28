public class DiveModelProfileClone
{
    public IDiveModelProfile Clone(IDiveModelProfile diveModelProfile)
    {
        return new DiveModelProfile()
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
            CompartmentLoads = diveModelProfile.CompartmentLoads,
        };
    }
}