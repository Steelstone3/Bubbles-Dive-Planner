namespace DivePlannerMk3.Contracts
{
    public interface IGasManagementModel
    {
        int CylinderVolume
        {
            get; set;
        }
        int CylinderPressure
        {
            get; set;
        }

        int SacRate
        {
            get; set;
        }

        int InitialCylinderTotalVolume
        {
            get; set;
        }

        int GasUsedForStep
        {
            get; set;
        }

        int GasRemaining
        {
            get; set;
        }
    }
}
