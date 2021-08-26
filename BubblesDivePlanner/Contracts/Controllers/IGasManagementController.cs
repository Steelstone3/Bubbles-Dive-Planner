namespace BubblesDivePlanner.Contracts.Controllers
{
    interface IGasManagementController
    {
        int CalculateInitialGasVolume(int cylinderVolume, int cylinderPressure);
        int CalculateGasRemaining(int gasRemaining, int gasUsed);
        int CalculateGasUsed(int depth, int time, int sacRate);
        int ConvertToBar( int cylinderTotalVolume, int cylinderSize );
    }
}
