// TODO AH Test
public class DalDiveModelProfile
{
    public float OxygenAtPressure { get; set; }
    public float NitrogenAtPressure { get; set; }
    public float HeliumAtPressure { get; set; }
    public float[] NitrogenTissuePressures { get; set; }
    public float[] HeliumTissuePressures { get; set; }
    public float[] TotalTissuePressures { get; set; }
    public float[] AValues { get; set; }
    public float[] BValues { get; set; }
    public float[] ToleratedAmbientPressures { get; set; }
    public float[] MaxSurfacePressures { get; set; }
    public float[] CompartmentLoads { get; set; }
}