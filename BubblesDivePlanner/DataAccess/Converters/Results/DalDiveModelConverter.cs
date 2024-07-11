// TODO AH Test
public class DalDiveModelConverter : IDalConverter<DalDiveModel, IDiveModel>
{
    public IDiveModel ConvertFrom(DalDiveModel dalDiveModel)
    {
        throw new NotImplementedException();
    }

    public DalDiveModel ConvertTo(IDiveModel diveModel)
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