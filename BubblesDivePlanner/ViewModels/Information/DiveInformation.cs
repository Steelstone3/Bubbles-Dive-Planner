using System.Collections.ObjectModel;
using ReactiveUI;

public class DiveInformation : ReactiveObject, IDiveInformation
{
    private float diveCeiling;
    public float DiveCeiling
    {
        get => diveCeiling;
        set => this.RaiseAndSetIfChanged(ref diveCeiling, value);
    }

    public ObservableCollection<IDiveStep> DecompressionSteps
    {
        get;
    } = new ObservableCollection<IDiveStep>();
}

public interface IDiveInformation
{
    float DiveCeiling { get; set; }
    ObservableCollection<IDiveStep> DecompressionSteps { get; }
}