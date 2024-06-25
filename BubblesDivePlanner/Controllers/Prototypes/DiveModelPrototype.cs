public class DiveModelPrototype : IDiveModelPrototype
{
    public IDiveModel DeepClone(IDiveModel diveModel)
    {
        return new DiveModel
        {
            Name = diveModel.Name,
            CompartmentCount = diveModel.CompartmentCount,
            NitrogenHalfTime = diveModel.NitrogenHalfTime,
            HeliumHalfTime = diveModel.HeliumHalfTime,
            AValuesNitrogen = diveModel.AValuesNitrogen,
            BValuesNitrogen = diveModel.BValuesNitrogen,
            AValuesHelium = diveModel.AValuesHelium,
            BValuesHelium = diveModel.BValuesHelium,
            DiveModelProfile = DiveModelProfileDeepClone(diveModel.DiveModelProfile)
        };
    }

    private IDiveModelProfile DiveModelProfileDeepClone(IDiveModelProfile diveModelProfile)
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

public interface IDiveModelPrototype
{
    IDiveModel DeepClone(IDiveModel diveModel);
}