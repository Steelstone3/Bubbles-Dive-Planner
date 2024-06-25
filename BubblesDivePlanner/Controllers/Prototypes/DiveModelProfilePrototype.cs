public class DiveModelProfilePrototype : IDiveModelProfilePrototype
{
    public IDiveModelProfile DeepClone(IDiveModelProfile diveModelProfile)
    {
        return new DiveModelProfile(0)
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

public interface IDiveModelProfilePrototype
{
    IDiveModelProfile DeepClone(IDiveModelProfile diveModelProfile);
}