// TODO AH Test
public class DalDiveStageConverter : IDalConverter<DalDiveStage, IDiveStage>
{
    public IDiveStage ConvertFrom(DalDiveStage dalDiveStage)
    {
        throw new NotImplementedException();
    }

    public DalDiveStage ConvertTo(IDiveStage diveStage)
    {
        DalDiveModelConverter dalDiveModelConverter = new();
        DalDiveStepConverter dalDiveStepConverter = new();
        DalCylinderConverter dalCylinderConverter = new();

        return new()
        {
            DiveModel = dalDiveModelConverter.ConvertTo(diveStage.DiveModel),
            DiveStep = dalDiveStepConverter.ConvertTo(diveStage.DiveStep),
            Cylinder = dalCylinderConverter.ConvertTo(diveStage.Cylinder)
        };
    }
}