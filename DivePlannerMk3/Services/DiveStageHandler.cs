using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.Controllers.DiveStages;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveResult;

namespace DivePlannerMk3.Services
{
    public class DiveStageHandler
    {
        private IDiveModel _theDiveModel;
        private IDiveProfile _diveProfile;
        private PlanDiveStepViewModel _diveStep;
        private PlanGasMixtureViewModel _gasMixture;

        public DiveStageHandler(IDiveModel theDiveModel, IDiveProfile diveProfile, PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            _theDiveModel = theDiveModel;
            _diveProfile = diveProfile;
            _diveStep = diveStep;
            _gasMixture = gasMixture;
        }

        public DiveProfileResultsListViewModel RunAllDiveStages()
        {
            var outputResults = new DiveProfileResultsListViewModel();

//TODO AH tidy this up!
            IDiveStage[] preDiveStages = new IDiveStage[]
            {
                //TODO AH set up results
                new PreDiveStageStepInfo(outputResults, _theDiveModel, _diveStep, _gasMixture),
                new PreDiveStageAmbientPressure(_diveProfile, _gasMixture.SelectedGasMixture.Oxygen, _gasMixture.SelectedGasMixture.Helium, _diveStep.Depth),
            };

            //TODO AH take out the for loops in the stages
            IDiveStage[] diveStages = new IDiveStage[]
            {
                new DiveStageTissuePressure(_theDiveModel, _diveProfile, _diveStep.Time),
                new DiveStageABValues(_theDiveModel, _diveProfile),
                new DiveStageToleratedAmbientPressure(_theDiveModel,_diveProfile),
                new DiveStageMaximumSurfacePressure(_theDiveModel, _diveProfile),
                new DiveStageCompartmentLoad(_theDiveModel, _diveProfile),
                new DiveStageResults(outputResults, _diveProfile),
            };

            foreach (var stage in preDiveStages)
            {
                stage.RunStage();
            }

//TODO AH out of range exception in here tolerated ambient pressure onwards
            foreach (var stage in diveStages)
            {
                stage.Compartment = 0;

                for (int i = 0; i < _theDiveModel.CompartmentCount - 1; i++)
                {
                    stage.RunStage();
                    stage.Compartment++;
                }
            }

            return outputResults;
        }
    }
}
