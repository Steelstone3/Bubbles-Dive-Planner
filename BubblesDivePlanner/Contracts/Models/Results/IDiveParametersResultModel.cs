using BubblesDivePlanner.Contracts.Models.Plan;

namespace BubblesDivePlanner.Contracts.Models.Results
{
    public interface IDiveParametersResultModel : IDiveStepModel, IGasMixtureModel
    {
        string DiveProfileStepHeader
        {
            get; set;
        }

        string DiveModelUsed
        {
            get; set;
        }

        double DiveCeiling
        {
            get; set;
        }
    }
}