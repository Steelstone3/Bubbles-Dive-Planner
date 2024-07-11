// TODO AH Test
public class DalResultConverter : IDalConverter<DalResult, IResult>
{
    public IResult ConvertFrom(DalResult dalResult)
    {
        throw new NotImplementedException();
    }

    public DalResult ConvertTo(IResult result)
    {
        DalDiveStageConverter dalDiveStageConverter = new();

        DalResult dalResult = new();

        List<DalDiveStage> dalDiveStages = [];
        foreach (IDiveStage diveStage in result.Results)
        {
            dalDiveStages.Add(dalDiveStageConverter.ConvertTo(diveStage));
        }
        dalResult.Results = dalDiveStages.ToArray();

        return dalResult;
    }
}