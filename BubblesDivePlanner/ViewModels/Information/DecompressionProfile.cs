using System.Collections.ObjectModel;
using ReactiveUI;

public class DecompressionProfile : ReactiveObject
{
    private float diveCeiling;
    public float DiveCeiling
    {
        get => diveCeiling;
        set => this.RaiseAndSetIfChanged(ref diveCeiling, value);
    }

    public ObservableCollection<DiveStep> DecompressionSteps
    {
        get;
    } = new ObservableCollection<DiveStep>();
}