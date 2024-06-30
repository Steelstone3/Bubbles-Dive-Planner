using System.Collections.ObjectModel;
using ReactiveUI;

public class Result : ReactiveObject, IResult
{
    public ObservableCollection<IDiveStage> Results
    {
        get;
    } = [];

    private float diveCeiling;
    public float DiveCeiling
    {
        get => diveCeiling;
        set => this.RaiseAndSetIfChanged(ref diveCeiling, value);
    }
}

public interface IResult
{
    ObservableCollection<IDiveStage> Results { get; }
    float DiveCeiling { get; set; }
}