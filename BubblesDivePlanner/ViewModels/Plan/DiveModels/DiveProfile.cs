using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;

namespace BubblesDivePlanner.ViewModels.Plan.DiveModels
{
    public class DiveProfile : IDiveProfile
    {
        private readonly int compartments;

        public DiveProfile(int compartments)
        {
            this.compartments = compartments;
            MaxSurfacePressures = new double[compartments];
            NitrogenTissuePressures = new double[compartments];
            HeliumTissuePressures = new double[compartments];
            TotalTissuePressures = new double[compartments];
            ToleratedAmbientPressures = new double[compartments];
            AValues = new double[compartments];
            BValues = new double[compartments];
            CompartmentLoads = new double[compartments];
        }

        public double[] MaxSurfacePressures { get; }

        public double[] NitrogenTissuePressures { get; }

        public double[] HeliumTissuePressures { get; }

        public double[] TotalTissuePressures { get; }

        public double[] ToleratedAmbientPressures { get; }

        public double[] AValues { get; }

        public double[] BValues { get; }

        public double[] CompartmentLoads { get; }

        public double OxygenAtPressure { get; }

        public double NitrogenAtPressure { get; }

        public double HeliumAtPressure { get; }

        
    }
}