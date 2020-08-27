using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class GasManagementModel : IGasManagementModel
    {
        #region Gas Setup
        public int CylinderVolume
        {
            get; set;
        }
        public int CylinderPressure
        {
            get; set;
        }

        public int SacRate
        {
            get; set;
        }

        public int InitialCylinderTotalVolume
        {
            get; set;
        }

        #endregion

        #region Gas Usage
        public int GasUsedForStep
        {
            get; set;
        }
        public int GasRemaining
        {
            get; set;
        }

        #endregion
    }
}

