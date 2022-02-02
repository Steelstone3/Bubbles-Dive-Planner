using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture
{
    public interface IGasMixtureModel : IVisibility
    {
        double Oxygen { get; set; }
        double Helium { get; set; }
        double Nitrogen { get; }
    }
}