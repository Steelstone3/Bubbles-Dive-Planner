namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IGasMixture
    {
        float Oxygen { get; set; }
        float Helium { get; set; }
        float Nitrogen { get; }
        float MaximumOperatingDepth { get; }
    }
}