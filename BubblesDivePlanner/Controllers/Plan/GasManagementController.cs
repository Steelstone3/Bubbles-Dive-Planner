using BubblesDivePlanner.Contracts.Controllers;

namespace BubblesDivePlanner.Controllers.Plan
{
    public class GasManagementController : IGasManagementController
    {
        public int CalculateGasRemaining(int gasRemaining, int gasUsed)
        {
            return gasRemaining - gasUsed;
        }

        public int CalculateGasUsed( int depth, int time, int sacRate )
        {
            return ( ( depth / 10 ) + 1 ) * time * sacRate;
        }

        public int CalculateInitialGasVolume(int cylinderVolume, int cylinderPressure)
        {
            return cylinderVolume * cylinderPressure;
        }

        public int ConvertToBar( int cylinderTotalVolume, int cylinderSize )
        {
            return cylinderTotalVolume / cylinderSize;
        }
    }
}
