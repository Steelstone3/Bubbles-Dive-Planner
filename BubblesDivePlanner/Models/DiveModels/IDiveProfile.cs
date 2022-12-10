namespace BubblesDivePlanner.Models.DiveModels
{
    public interface IDiveProfile
    {
        double[] NitrogenTissuePressures { get; }
        double[] HeliumTissuePressures { get; }
        double[] TotalTissuePressures { get; }
        double[] MaxSurfacePressures { get; }
        double[] ToleratedAmbientPressures { get; }
        double[] AValues { get; }
        double[] BValues { get; }
        double[] CompartmentLoads { get; }
        double OxygenPressureAtDepth { get; }
        double HeliumPressureAtDepth { get; }
        double NitrogenPressureAtDepth { get; }
        double DepthCeiling { get; }
        void UpdateDepthCeiling();
        void UpdateGasMixtureUnderPressure
        (
            double oxygenPressureAtDepth,
            double heliumPressureAtDepth,
            double nitrogenPressureAtDepth
        );
    }
}