// TODO AH Test
public class DalDiveStepConverter : IDalConverter<DalDiveStep, DiveStep>
{
    public DiveStep ConvertFrom(DalDiveStep dalDiveStep)
    {
        return new DiveStep()
        {
            Depth = dalDiveStep.Depth,
            Time = dalDiveStep.Time,
        };
    }

    public DalDiveStep ConvertTo(DiveStep diveStep)
    {
        return new()
        {
            Depth = diveStep.Depth,
            Time = diveStep.Time,
        };
    }
}