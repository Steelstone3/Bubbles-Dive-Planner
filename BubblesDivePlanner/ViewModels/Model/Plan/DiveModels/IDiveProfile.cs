namespace BubblesDivePlanner.ViewModels.Model.Plan.DiveModels
{
    public interface IDiveProfile
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
}