using BubblesDivePlanner.GasManagement.GasMixture;
using BubblesDivePlanner.GasManagement.GasUsage;

namespace BubblesDivePlanner.GasManagement.Cylinder
{
    public interface ICylinderModel
    {
        int CylinderVolume {get;set;}
        int CylinderPressure {get;set;}
        IGasMixtureModel GasMixture {get;set;}
        IGasUsageModel GasUsage {get;set;}
    }
}