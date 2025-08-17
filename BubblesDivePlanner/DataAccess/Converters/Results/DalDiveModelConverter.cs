// TODO AH Test
public class DalDiveModelConverter : IDalConverter<DalDiveModel, DiveModel>
{
    public DiveModel ConvertFrom(DalDiveModel dalDiveModel)
    {
        DalDiveModelProfileConverter dalDiveModelProfileConverter = new();

        return new DiveModel()
        {
            Name = dalDiveModel.Name,
            CompartmentCount = dalDiveModel.CompartmentCount,
            NitrogenHalfTime = dalDiveModel.NitrogenHalfTime,
            HeliumHalfTime = dalDiveModel.HeliumHalfTime,
            AValuesNitrogen = dalDiveModel.AValuesNitrogen,
            BValuesNitrogen = dalDiveModel.BValuesNitrogen,
            AValuesHelium = dalDiveModel.AValuesHelium,
            BValuesHelium = dalDiveModel.BValuesHelium,
            DiveModelProfile = dalDiveModelProfileConverter.ConvertFrom(dalDiveModel.DiveModelProfile),
        };
    }

    public DalDiveModel ConvertTo(DiveModel diveModel)
    {
        DalDiveModelProfileConverter dalDiveModelProfileConverter = new();

        return new()
        {
            Name = diveModel.Name,
            CompartmentCount = diveModel.CompartmentCount,
            NitrogenHalfTime = diveModel.NitrogenHalfTime,
            HeliumHalfTime = diveModel.HeliumHalfTime,
            AValuesNitrogen = diveModel.AValuesNitrogen,
            BValuesNitrogen = diveModel.BValuesNitrogen,
            AValuesHelium = diveModel.AValuesHelium,
            BValuesHelium = diveModel.BValuesHelium,
            DiveModelProfile = dalDiveModelProfileConverter.ConvertTo(diveModel.DiveModelProfile)
        };
    }
}