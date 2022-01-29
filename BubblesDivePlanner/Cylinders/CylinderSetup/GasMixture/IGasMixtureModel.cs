namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture
{
    public interface IGasMixtureModel
    {
        double Oxygen { get; set; }
        double Helium { get; set; }
        double Nitrogen { get; }
    }
}