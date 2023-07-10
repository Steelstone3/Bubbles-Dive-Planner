namespace BubblesDivePlanner.ViewModels.Models.Plan
{
    public interface ICylinder
    {
        string Name { get; set; }
        ushort Volume { get; set; }
        ushort Pressure { get; set; }
        ushort InitialPressurisedVolume { get; set; }
        IGasMixture GasMixture { get; set; }
        IGasUsage GasUsage { get; set; }
    }
}