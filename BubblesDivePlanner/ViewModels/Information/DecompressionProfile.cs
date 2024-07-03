using System.Collections.ObjectModel;
using ReactiveUI;

public class DecompressionProfile : ReactiveObject, IDecompressionProfile
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

public interface IDecompressionProfile
{
    float DiveCeiling { get; set; }
    ObservableCollection<IDiveStep> DecompressionSteps { get; }
}