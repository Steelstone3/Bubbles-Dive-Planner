// TODO AH Test
public class DalDiveStepConverter : IDalConverter<DalDiveStep, IDiveStep>
{
    public IDiveStep ConvertFrom(DalDiveStep dalDiveStep)
    {
        return new DiveStep(new DiveStepValidator())
        {
            Depth = dalDiveStep.Depth,
            Time = dalDiveStep.Time,
        };
    }

    public DalDiveStep ConvertTo(IDiveStep diveStep)
    {
        return new()
        {
            Depth = diveStep.Depth,
            Time = diveStep.Time,
        };
    }
}