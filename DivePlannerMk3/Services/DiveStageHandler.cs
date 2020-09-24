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
        private IDiveStage[] _postDiveStages;

        //updated using UpdateDiveStageHandler()
        private IDiveModel _diveModel;
        private PlanDiveStepViewModel _diveStep;
        private PlanGasMixtureViewModel _gasMixture;

        public IDiveProfile DiveProfile
        {
            get; private set;
        }

        public DiveProfileResultsListViewModel RunAllDiveStages()
        {
            _outputResults = new DiveProfileResultsListViewModel();
            _preDiveStages = CreatePreDiveStages();
            _diveStages = CreateDiveStages();
            _postDiveStages = CreatePostDiveStages();

            RunStages();

            return _outputResults;
        }

        public void UpdateDiveStageHandler(IDiveModel diveModel, IDiveProfile diveProfile, PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            _diveModel = diveModel;
            DiveProfile = diveProfile;
            _diveStep = diveStep;
            _gasMixture = gasMixture;
        }

        private void RunStages()
        {
            foreach (var stage in _preDiveStages)
            {
                stage.RunStage();
            }

            //For each stage run for the amount of compartments
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                _diveStages[0].RunStage();
                _diveStages[1].RunStage();
                _diveStages[2].RunStage();
                _diveStages[3].RunStage();
                _diveStages[4].RunStage();
                _postDiveStages[0].RunStage();
            }
        }

        private IDiveStage[] CreatePreDiveStages()
        {
            return new IDiveStage[]
            {
                new PreDiveStageStepInfo(_outputResults, _diveModel, _diveStep, _gasMixture),
                new PreDiveStageAmbientPressure(DiveProfile, _gasMixture.SelectedGasMixture.Oxygen, _gasMixture.SelectedGasMixture.Helium, _diveStep.Depth),
            };
        }


        private IDiveStage[] CreateDiveStages()
        {
            return new IDiveStage[]
            {
                new DiveStageTissuePressure(_diveModel, DiveProfile, _diveStep.Time),
                new DiveStageABValues(_diveModel, DiveProfile),
                new DiveStageToleratedAmbientPressure(_diveModel.CompartmentCount,DiveProfile),
                new DiveStageMaximumSurfacePressure(_diveModel.CompartmentCount, DiveProfile),
                new DiveStageCompartmentLoad(_diveModel, DiveProfile),
            };
        }

        private IDiveStage[] CreatePostDiveStages()
        {
            return new IDiveStage[]
            {
                new DiveStageResults(_diveModel.CompartmentCount,_outputResults, DiveProfile)
            };
        }
    }
}
