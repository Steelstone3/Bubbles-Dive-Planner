using System.Collections.Generic;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;

namespace DivePlannerMk3.Contracts
{
    public interface IDiveProfileService
    {
        IDiveModel TheDiveModel
        {
            get; set;
        }
        DiveProfileResultsListViewModel RunDiveStep(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture);
        //IEnumerable<DiveProfileResultsListViewModel> RunDecompressionDiveSteps( InfoDecompressionProfileViewModel decompressionDiveSteps, PlanGasMixtureViewModel gasMixture );
    }
}