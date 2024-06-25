using System.Collections.ObjectModel;

public class Result : IResult
{
    public ObservableCollection<IDiveStage> Results
    {
        get;
    } = [];
}

public interface IResult
{
    ObservableCollection<IDiveStage> Results { get; }
}