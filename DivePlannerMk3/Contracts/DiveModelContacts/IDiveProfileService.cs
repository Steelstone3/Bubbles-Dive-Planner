using DivePlannerMk3.Models;

namespace DivePlannerMk3.Contracts
{
    public interface IDiveProfileService
    {
        IDiveModel TheDiveModel
        {
            get; set;
        }
        DiveResultsModel RunDiveStep(DiveStepModel diveStep, GasMixtureModel gasMixture);
        DiveParametersOutputModel UpdateParametersUsed(DiveStepModel diveStep, GasMixtureModel gasMixture);
        
        //IEnumerable<DiveProfileResultsListViewModel> RunDecompressionDiveSteps( InfoDecompressionProfileViewModel decompressionDiveSteps, PlanGasMixtureViewModel gasMixture );
    }
}