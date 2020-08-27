using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Controllers
{
    class GasManagementController : IGasManagementController
    {
        public int CalculateGasUsed( int depth, int time, int sacRate )
        {
            return ( ( depth / 10 ) + 1 ) * time * sacRate;
        }

        public int ConvertToBar( int cylinderTotalVolume, int cylinderSize )
        {
            return cylinderTotalVolume / cylinderSize;
        }
    }
}
