namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture
{
    public class GasMixtureController : IGasMixtureController
    {
        public double CalculateNitrogenMixture(double oxygen, double helium)
        {
            return 100 - oxygen - helium;
        }
    }
}