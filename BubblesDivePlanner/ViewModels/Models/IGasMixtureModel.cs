namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IGasMixtureModel : IVisibility
    {
        double Oxygen { get; set; }
        double Helium { get; set; }
        double Nitrogen { get; }
        double MaximumOperatingDepth { get; }
    }
}