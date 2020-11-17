using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class PreDiveStageStepInfo : IDiveStage
    {
        private DiveResultsModel _outputModel;
        private IDiveModel _diveModel;
        private PlanDiveStepViewModel _diveStep;
        private PlanGasMixtureViewModel _gasMixture;

        public PreDiveStageStepInfo(DiveResultsModel outputModel, IDiveModel diveModel, PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            _outputModel = outputModel;
            _diveModel = diveModel;
            _diveStep = diveStep;
            _gasMixture = gasMixture;
        }

        public void RunStage()
        {
            PopulateHeader();
            PopulateDiveStepParameters();
        }

        private void PopulateHeader() => _outputModel.DiveParametersOutput.DiveProfileStepHeader = _diveModel.DiveModelName + "\r\nDepth: " + _diveStep.Depth.ToString() + " Time: " + _diveStep.Time.ToString();

        private void PopulateDiveStepParameters()
        {
            _outputModel.DiveParametersOutput.DiveModelUsed = _diveModel.DiveModelName;
            _outputModel.DiveParametersOutput.DiveDepthUsed = _diveStep.Depth;
            _outputModel.DiveParametersOutput.DiveTimeUsed = _diveStep.Time;
            _outputModel.DiveParametersOutput.GasMixNameUsed = _gasMixture.SelectedGasMixture.GasName;

            //TODO AH add when gas management is integrated
            //_outputModel.DiveParametersOutput.GasUsedParameter;
            //_outputModel.DiveParametersOutput.GasRemainingParameter;
        }
    }
}
















