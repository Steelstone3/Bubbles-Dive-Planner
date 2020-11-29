using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class PreDiveStageStepInfo : IDiveStage
    {
        private GasManagementController _gasManagementController;
        private DiveParametersOutputModel _diveParametersModel;
        private IDiveModel _diveModel;
        private DiveStepModel _diveStep;
        private GasMixtureModel _gasMixture;

        public PreDiveStageStepInfo(DiveParametersOutputModel diveParametersModel, IDiveModel diveModel, DiveStepModel diveStep, GasMixtureModel gasMixture)
        {
            _diveParametersModel = diveParametersModel;
            _diveModel = diveModel;
            _diveStep = diveStep;
            _gasMixture = gasMixture;

            _gasManagementController = new GasManagementController();
        }

        public void RunStage()
        {
            PopulateHeader();
            PopulateDiveStepParameters();
        }

        private void PopulateHeader() => _diveParametersModel.DiveProfileStepHeader = _diveModel.DiveModelName + "\r\nDepth: " + _diveStep.Depth.ToString() + " Time: " + _diveStep.Time.ToString();

        private void PopulateDiveStepParameters()
        {
            _diveParametersModel.DiveModelUsed = _diveModel.DiveModelName;

            _diveParametersModel.DiveStepModel.Depth = _diveStep.Depth;
            _diveParametersModel.DiveStepModel.Time = _diveStep.Time;

            _diveParametersModel.GasMixtureModel.GasName = _gasMixture.GasName;

            //TODO AH add when gas management is integrated
            //Inject gas managment controller to do the calculations
            //_diveParametersModel.GasUsedParameter;
            //_diveParametersModel.GasRemainingParameter;
        }
    }
}
















