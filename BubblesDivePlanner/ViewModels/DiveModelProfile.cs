using ReactiveUI;

public class DiveModelProfile : ReactiveObject, IDiveModelProfile
{
    public DiveModelProfile(byte compartmentCount)
    {
    }
    
    private float oxygenAtPressure;
    public float OxygenAtPressure
    {
        get => oxygenAtPressure;
        set => this.RaiseAndSetIfChanged(ref oxygenAtPressure, value);
    }

    private float nitrogenAtPressure;
    public float NitrogenAtPressure
    {
        get => nitrogenAtPressure;
        set => this.RaiseAndSetIfChanged(ref nitrogenAtPressure, value);
    }

    private float heliumAtPressure;
    public float HeliumAtPressure
    {
        get => heliumAtPressure;
        set => this.RaiseAndSetIfChanged(ref heliumAtPressure, value);
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

    public float[] toleratedAmbientPressures;
    public float[] ToleratedAmbientPressures
    {
        get => toleratedAmbientPressures;
        set => this.RaiseAndSetIfChanged(ref toleratedAmbientPressures, value);
    }

    public float[] maxSurfacePressures;
    public float[] MaxSurfacePressures
    {
        get => maxSurfacePressures;
        set => this.RaiseAndSetIfChanged(ref maxSurfacePressures, value);
    }
    public float[] compartmentLoads;
    public float[] CompartmentLoads
    {
        get => compartmentLoads;
        set => this.RaiseAndSetIfChanged(ref compartmentLoads, value);
    }
}

public interface IDiveModelProfile
{
    float[] MaxSurfacePressures { get; set; }
    float[] NitrogenTissuePressures { get; set; }
    float[] HeliumTissuePressures { get; set; }
    float[] TotalTissuePressures { get; set; }
    float[] ToleratedAmbientPressures { get; set; }
    float[] AValues { get; set; }
    float[] BValues { get; set; }
    float[] CompartmentLoads { get; set; }
    float OxygenAtPressure { get; set; }
    float NitrogenAtPressure { get; set; }
    float HeliumAtPressure { get; set; }
}