using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;

namespace BubblesDivePlanner.Contracts.Models.Plan
{
    public interface IGasMixtureSelectorModel
    {
        double MaximumOperatingDepth { get; set; }

        IGasMixtureModel SelectedGasMixture { get; set; }

        IGasMixtureViewModel NewGasMixture { get; set; }
    }
}