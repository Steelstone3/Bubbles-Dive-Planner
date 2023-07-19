namespace BubblesDivePlanner.ViewModels.Model.Planner.Cylinders
{
    public interface IGasMixture
    {
        float Oxygen { get; set; }
        float Helium { get; set; }
        float Nitrogen { get; }
        float MaximumOperatingDepth { get; }
    }
}