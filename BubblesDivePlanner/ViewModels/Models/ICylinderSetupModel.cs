namespace BubblesDivePlanner.ViewModels.Models
{
    public interface ICylinderSetupModel : IVisibility
    {
        string CylinderName { get; set; }
        byte CylinderVolume { get; set; }
        ushort CylinderPressure { get; set; }
        ushort InitialPressurisedCylinderVolume { get; set; }
        IGasMixtureModel GasMixture { get; set; }
        IGasUsageModel GasUsage { get; set; }
    }
}