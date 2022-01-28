namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture
{
    public class GasMixtureController : IGasMixtureController
    {
        public int CalculateNitrogenMixture(int oxygen, int helium)
        {
            return 100 - oxygen - helium;
        }
    }
}