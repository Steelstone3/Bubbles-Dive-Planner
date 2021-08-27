using System.Collections.Generic;
using System.Linq;
using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Contracts.Commands;
using BubblesDivePlanner.Contracts.Models.DiveModels;
using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Models.Results;

namespace BubblesDivePlanner.Services
{
    public class DiveStageHandler
    {
        private DiveResultsStepOutputModel _outputResultsStepOutput;
        private IDiveStage[] _preDiveStages;
        private IDiveStage[] _diveStages;

        //updated using UpdateDiveStageHandler()
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;
        private IDiveStepModel _diveStep;
        private IGasMixtureModel _selectedGasMixture;

        public DiveResultsStepOutputModel RunDiveStages()
        {
            _outputResultsStepOutput = new DiveResultsStepOutputModel();
            _diveStages = CreateDiveStages();
            _preDiveStages = CreatePreDiveStages();

            RunStages();

            return _outputResultsStepOutput;
        }

        public DiveParametersResultModel UpdateUsedDiveParameters(IDiveStepModel diveStep, IGasMixtureModel selectedGasMixture, IGasManagementModel gasManagementModel)
        {
            var diveParameters = new DiveParametersResultModel();

            var stepInfo = new PostDiveStageStepInfo(diveParameters, _diveModel, diveStep, selectedGasMixture, gasManagementModel, GetToleratedAmbientPressures().ToList());

            stepInfo.RunStage();

            return diveParameters;
        }

        public void UpdateDiveStageHandler(IDiveModel diveModel, IDiveProfile diveProfile, IDiveStepModel diveStep, IGasMixtureModel selectedGasMixture)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
            _diveStep = diveStep;
            _selectedGasMixture = selectedGasMixture;
        }

        private void RunStages()
        {
            foreach (var preDiveStage in _preDiveStages)
            {
                preDiveStage.RunStage();
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
                new PreDiveStageAmbientPressure(_diveProfile, _selectedGasMixture.Oxygen, _selectedGasMixture.Helium, _diveStep.Depth),
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
                new DiveStageResults(_diveModel.CompartmentCount,_outputResultsStepOutput, _diveProfile)
            };
        }

        private IEnumerable<double> GetToleratedAmbientPressures()
        {
            return _outputResultsStepOutput.DiveProfileStepOutput.Select(diveProfile => diveProfile.ToleratedAmbientPressureResult);
        }
    }
}
