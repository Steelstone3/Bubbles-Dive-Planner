using System.Collections.ObjectModel;
using ReactiveUI;

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