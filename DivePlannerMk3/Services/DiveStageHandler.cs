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
        private DiveProfileResultsListViewModel _outputResults;
        private IDiveStage[] _preDiveStages;
        private IDiveStage[] _diveStages;

        //updated using UpdateDiveStageHandler()
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;
        private PlanDiveStepViewModel _diveStep;
        private PlanGasMixtureViewModel _gasMixture;

        public DiveProfileResultsListViewModel RunAllDiveStages()
        {
            _outputResults = new DiveProfileResultsListViewModel();
            _preDiveStages = CreatePreDiveStages();
            _diveStages = CreateDiveStages();

            RunStages();

            return _outputResults;
        }

        public void UpdateDiveStageHandler(IDiveModel diveModel, IDiveProfile diveProfile, PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
            _diveStep = diveStep;
            _gasMixture = gasMixture;
        }

        private void RunStages()
        {
            foreach (var stage in _preDiveStages)
            {
                stage.RunStage();
            }

            //For each compartment run all stages
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                foreach (var diveStage in _diveStages)
                {
                    diveStage.RunStage();
                }
            }
        }

        private IDiveStage[] CreatePreDiveStages()
        {
            return new IDiveStage[]
            {
                new PreDiveStageStepInfo(_outputResults, _diveModel, _diveStep, _gasMixture),
                new PreDiveStageAmbientPressure(_diveProfile, _gasMixture.SelectedGasMixture.Oxygen, _gasMixture.SelectedGasMixture.Helium, _diveStep.Depth),
            };
        }

        private IDiveStage[] CreateDiveStages()
        {
            return new IDiveStage[]
            {
                new DiveStageTissuePressure(_diveModel, _diveProfile, _diveStep.Time),
                new DiveStageABValues(_diveModel, _diveProfile),
                new DiveStageToleratedAmbientPressure(_diveModel.CompartmentCount,_diveProfile),
                new DiveStageMaximumSurfacePressure(_diveModel.CompartmentCount, _diveProfile),
                new DiveStageCompartmentLoad(_diveModel, _diveProfile),
                new DiveStageResults(_diveModel.CompartmentCount,_outputResults, _diveProfile)
            };
        }
    }
}
