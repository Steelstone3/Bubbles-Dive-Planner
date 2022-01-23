namespace BubblesDivePlanner.GasManagement.GasMixture
{
    public interface IGasMixtureModel
    {
        int Oxygen { get; set; }
        int Helium { get; set; }
        int Nitrogen { get; }
    }
}