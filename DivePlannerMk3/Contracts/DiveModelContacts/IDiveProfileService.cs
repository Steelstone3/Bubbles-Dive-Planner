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
        DiveParametersOutputModel UpdateParametersUsed(IDiveStepModel diveStep, IGasMixtureModel gasMixture);
        
        //IEnumerable<DiveProfileResultsListViewModel> RunDecompressionDiveSteps( InfoDecompressionProfileViewModel decompressionDiveSteps, PlanGasMixtureViewModel gasMixture );
    }
}