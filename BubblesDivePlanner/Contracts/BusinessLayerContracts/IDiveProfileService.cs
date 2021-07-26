using DivePlannerMk3.Models;

namespace DivePlannerMk3.Contracts
{
    public interface IDiveProfileService
    {
        IDiveModel TheDiveModel
        {
            get; set;
        }
        DiveResultsModel RunDiveStep(IDiveStepModel diveStep, IGasMixtureModel gasMixture);
        IDiveParametersResultModel UpdateParametersUsed(IDiveStepModel diveStep, IGasMixtureModel gasMixture, IGasManagementModel gasManagementSetup);
        
        //IEnumerable<DiveProfileResultsListViewModel> RunDecompressionDiveSteps( InfoDecompressionProfileViewModel decompressionDiveSteps, PlanGasMixtureViewModel gasMixture );
    }
}