public class DiveModelClone
{
    public IDiveModel Clone(IDiveModel diveModel)
    {
        return new DiveModel()
        {
            Name = diveModel.Name,
            CompartmentCount = diveModel.CompartmentCount,
            NitrogenHalfTime = diveModel.NitrogenHalfTime,
            HeliumHalfTime = diveModel.HeliumHalfTime,
            AValuesNitrogen = diveModel.AValuesNitrogen,
            BValuesNitrogen = diveModel.BValuesNitrogen,
            AValuesHelium = diveModel.AValuesHelium,
            BValuesHelium = diveModel.BValuesHelium,
            DiveModelProfile = new DiveModelProfileClone().Clone(diveModel.DiveModelProfile),
        };
    }
}