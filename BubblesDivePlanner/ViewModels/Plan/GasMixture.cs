using BubblesDivePlanner.ViewModels.Models.Plan;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class GasMixture : IGasMixture
    {
        public float Oxygen
        {
            get;
            set;
        }
        public float Helium
        {
            get;
            set;
        }

        public float Nitrogen
        {
            get => 100 - Oxygen - Helium;
        }

        public float MaximumOperatingDepth
        {
            get;
            set;
        }
    }
}