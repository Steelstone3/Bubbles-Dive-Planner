using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.DiveModels;

public class DiveProfile : ViewModelBase, IDiveProfile
{
    public DiveProfile(int compartments)
    {
        MaxSurfacePressures = new float[compartments];
        NitrogenTissuePressures = new float[compartments];
        HeliumTissuePressures = new float[compartments];
        TotalTissuePressures = new float[compartments];
        ToleratedAmbientPressures = new float[compartments];
        AValues = new float[compartments];
        BValues = new float[compartments];
        CompartmentLoads = new float[compartments];

        for (int i = 0; i < compartments; i++)
        {
            NitrogenTissuePressures[i] = 0.79f;
            TotalTissuePressures[i] = 0.79f;
        }
    }

    public float[] maxSurfacePressures;
    public float[] MaxSurfacePressures
    {
        get => maxSurfacePressures;
        set => this.RaiseAndSetIfChanged(ref maxSurfacePressures, value);
    }

    public float[] nitrogenTissuePressures;
    public float[] NitrogenTissuePressures
    {
        get => nitrogenTissuePressures;
        set => this.RaiseAndSetIfChanged(ref nitrogenTissuePressures, value);
    }

    public float[] heliumTissuePressures;
    public float[] HeliumTissuePressures
    {
        get => heliumTissuePressures;
        set => this.RaiseAndSetIfChanged(ref heliumTissuePressures, value);
    }

    public float[] totalTissuePressures;
    public float[] TotalTissuePressures
    {
        get => totalTissuePressures;
        set => this.RaiseAndSetIfChanged(ref totalTissuePressures, value);
    }

    public float[] toleratedAmbientPressures;
    public float[] ToleratedAmbientPressures
    {
        get => toleratedAmbientPressures;
        set => this.RaiseAndSetIfChanged(ref toleratedAmbientPressures, value);
    }

    public float[] aValues;
    public float[] AValues
    {
        get => aValues;
        set => this.RaiseAndSetIfChanged(ref aValues, value);
    }

    public float[] bValues;
    public float[] BValues
    {
        get => bValues;
        set => this.RaiseAndSetIfChanged(ref bValues, value);
    }

    public float[] compartmentLoads;
    public float[] CompartmentLoads
    {
        get => compartmentLoads;
        set => this.RaiseAndSetIfChanged(ref compartmentLoads, value);
    }

    public float oxygenAtPressure;
    public float OxygenAtPressure
    {
        get => oxygenAtPressure;
        set => this.RaiseAndSetIfChanged(ref oxygenAtPressure, value);
    }

    public float nitrogenAtPressure;
    public float NitrogenAtPressure
    {
        get => nitrogenAtPressure;
        set => this.RaiseAndSetIfChanged(ref nitrogenAtPressure, value);
    }

    public float heliumAtPressure;
    public float HeliumAtPressure
    {
        get => heliumAtPressure;
        set => this.RaiseAndSetIfChanged(ref heliumAtPressure, value);
    }
}