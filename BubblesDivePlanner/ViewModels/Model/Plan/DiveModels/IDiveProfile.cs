namespace BubblesDivePlanner.ViewModels.Model.Plan.DiveModels
{
    public interface IDiveProfile
    {
        double[] MaxSurfacePressures { get; }
        double[] NitrogenTissuePressures { get; }
        double[] HeliumTissuePressures { get; }
        double[] TotalTissuePressures { get; }
        double[] ToleratedAmbientPressures { get; }
        double[] AValues { get; }
        double[] BValues { get; }
        double[] CompartmentLoads { get; }
        double OxygenAtPressure { get; }
        double NitrogenAtPressure { get; }
        double HeliumAtPressure { get; }
    }
}