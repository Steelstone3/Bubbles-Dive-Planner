namespace BubblesDivePlanner.Contracts.Models.Plan
{
    public interface IGasMixtureModel
    {
        string GasName { get; set; }

        double Oxygen { get; set; }

        double Helium { get; set; }

        double Nitrogen { get; }
    }
}