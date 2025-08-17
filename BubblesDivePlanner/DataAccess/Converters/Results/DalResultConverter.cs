// TODO AH Test
using DynamicData;

public class DalResultConverter : IDalConverter<DalResult, Result>
{
    public Result ConvertFrom(DalResult dalResult)
    {
        DalDiveStageConverter dalDiveStageConverter = new();

        Result result = new();

        foreach (var item in dalResult.Results)
        {
            result.Results.Add(dalDiveStageConverter.ConvertFrom(item));
        }

        return result;
    }

    public DalResult ConvertTo(Result result)
    {
        DalDiveStageConverter dalDiveStageConverter = new();

        DalResult dalResult = new();

        List<DalDiveStage> dalDiveStages = [];
        foreach (DiveStage diveStage in result.Results)
        {
            dalDiveStages.Add(dalDiveStageConverter.ConvertTo(diveStage));
        }
        dalResult.Results = dalDiveStages.ToArray();

        return dalResult;
    }
}