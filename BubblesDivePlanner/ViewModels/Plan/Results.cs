using ReactiveUI;

public class Results : ReactiveObject, IResults
{
    private IDiveStage latestResult = new DiveStage();
    public IDiveStage LatestResult
    {
        get => latestResult;
        set => this.RaiseAndSetIfChanged(ref latestResult, value);
    }
}

public interface IResults
{
    IDiveStage LatestResult { get; set; }
}