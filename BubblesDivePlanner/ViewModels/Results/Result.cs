using ReactiveUI;

public class Result : ReactiveObject, IResult
{
    private IDiveStage results = new DiveStage(new DiveStageValidator());
    public IDiveStage Results
    {
        get => results;
        set => this.RaiseAndSetIfChanged(ref results, value);
    }
}

public interface IResult
{
    IDiveStage Results { get; set; }
}