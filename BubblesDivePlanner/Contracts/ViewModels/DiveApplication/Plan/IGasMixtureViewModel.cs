using BubblesDivePlanner.Contracts.Models.Plan;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IGasMixtureViewModel
    {
        string GasName { get; set; }

        double Oxygen { get; set; }

        double Helium { get; set; }

        double Nitrogen { get; }

        IGasMixtureModel Clone();
    }
}