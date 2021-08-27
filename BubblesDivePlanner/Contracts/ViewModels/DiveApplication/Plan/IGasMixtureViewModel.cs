using BubblesDivePlanner.Contracts.Models.Plan;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IGasMixtureViewModel : IGasMixtureModel
    {
        IGasMixtureModel Clone();
    }
}