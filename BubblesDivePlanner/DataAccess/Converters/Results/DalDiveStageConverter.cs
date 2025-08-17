// TODO AH Test
public class DalDiveStageConverter : IDalConverter<DalDiveStage, DiveStage>
{
    public DiveStage ConvertFrom(DalDiveStage dalDiveStage)
    {
        DalDiveModelConverter dalDiveModelConverter = new();
        DalDiveStepConverter dalDiveStepConverter = new();
        DalCylinderConverter dalCylinderConverter = new();

        return new DiveStage()
        {
            DiveModel = dalDiveModelConverter.ConvertFrom(dalDiveStage.DiveModel),
            DiveStep = dalDiveStepConverter.ConvertFrom(dalDiveStage.DiveStep),
            Cylinder = dalCylinderConverter.ConvertFrom(dalDiveStage.Cylinder),
        };
    }

    public DalDiveStage ConvertTo(DiveStage diveStage)
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