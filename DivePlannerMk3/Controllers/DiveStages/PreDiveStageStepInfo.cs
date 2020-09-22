using DivePlannerMk3.Contracts;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class PreDiveStageStepInfo : IDiveStage
    {
        private DiveProfileResultsListViewModel _outputResults;
        private IDiveModel _diveModel;
        private PlanDiveStepViewModel _diveStep;
        private PlanGasMixtureViewModel _gasMixture;

        //TODO AH base class of Dive Plan with common things such as this and an abstract RunStage method
        public int Compartment
        {
            get; set;
        }

        //TODO AH the constructor
        public PreDiveStageStepInfo(DiveProfileResultsListViewModel outputResults, IDiveModel diveModel, PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            _outputResults = outputResults;
            _diveModel = diveModel;
            _diveStep = diveStep;
            _gasMixture = gasMixture;
        }

        public void RunStage()
        {
            PopulateHeader();
            PopulateDiveStepParameters();
        }

        private void PopulateHeader() => _outputResults.DiveProfileStepHeader = _diveModel.DiveModelName + "\r\nDepth: " + _diveStep.Depth.ToString() + " Time: " + _diveStep.Time.ToString();

        private void PopulateDiveStepParameters()
        {
            _outputResults.ParameterOutput.DiveModelUsed = _diveModel.DiveModelName;
            _outputResults.ParameterOutput.StepDepthParameter = _diveStep.Depth;
            _outputResults.ParameterOutput.StepTimeParameter = _diveStep.Time;
            _outputResults.ParameterOutput.StepGasMixNameParameter = _gasMixture.SelectedGasMixture.GasName;

            //TODO AH add when gas management is integrated
            //_outputResults.ParameterOutput.GasUsedParameter
            //_outputResults.ParameterOutput.GasRemainingParameter
        }
    }
}
















