using BubblesDivePlanner.Contracts.Models.DiveModels;
using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Models.Results;

namespace BubblesDivePlanner.Contracts.Services
{
    public interface IDiveStageHandler
    {
        DiveResultsStepOutputModel RunDiveStages();

        DiveParametersResultModel UpdateUsedDiveParameters(IDiveStepModel diveStep,
            IGasMixtureModel selectedGasMixture, IGasManagementModel gasManagementModel);

        void UpdateDiveStageHandler(IDiveModel diveModel, IDiveProfile diveProfile, IDiveStepModel diveStep,
            IGasMixtureModel selectedGasMixture);
    }
}