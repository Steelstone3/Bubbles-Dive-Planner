using BubblesDivePlanner.Contracts.Models.DiveModels;
using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Contracts.Models.Results;
using BubblesDivePlanner.Models.Results;

namespace BubblesDivePlanner.Contracts.Services
{
    public interface IDiveProfileService
    {
        IDiveModel TheDiveModel
        {
            get; set;
        }
        DiveResultsStepOutputModel RunDiveStep(IDiveStepModel diveStep, IGasMixtureModel gasMixture);
        IDiveParametersResultModel UpdateParametersUsed(IDiveStepModel diveStep, IGasMixtureModel gasMixture, IGasManagementModel gasManagementSetup);
        
        //IEnumerable<DiveProfileResultsListViewModel> RunDecompressionDiveSteps( InfoDecompressionProfileViewModel decompressionDiveSteps, PlanGasMixtureViewModel gasMixture );
    }
}