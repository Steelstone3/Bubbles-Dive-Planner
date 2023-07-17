namespace BubblesDivePlanner.ViewModels.Model.Plan.DiveModels
{
    public interface IDiveProfile
    {
        double[] MaxSurfacePressures { get; set; }
        double[] NitrogenTissuePressures { get; set; }
        double[] HeliumTissuePressures { get; set; }
        double[] TotalTissuePressures { get; set; }
        double[] ToleratedAmbientPressures { get; set; }
        double[] AValues { get; set; }
        double[] BValues { get; set; }
        double[] CompartmentLoads { get; set; }
        double OxygenAtPressure { get; set; }
        double NitrogenAtPressure { get; set; }
        double HeliumAtPressure { get; set; }
    }
}