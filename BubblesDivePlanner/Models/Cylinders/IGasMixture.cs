namespace BubblesDivePlanner.Models.Cylinders
{
    public interface IGasMixture
    {
        byte Oxygen { get; }
        byte Helium { get; }
        byte Nitrogen { get; }
        double MaximumOperatingDepth { get; }
    }
}