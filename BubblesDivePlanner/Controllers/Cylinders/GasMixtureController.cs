using BubblesDivePlanner.Controllers.Interfaces;

namespace BubblesDivePlanner.Controllers.Cylinders
{
    public class GasMixtureController : IGasMixtureController
    {
        public double CalculateNitrogenMixture(double oxygen, double helium)
        {
            return 100 - oxygen - helium;
        }
    }
}