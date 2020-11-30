using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class PreDiveStageStepInfo : IDiveStage
    {
        //private GasManagementController _gasManagementController;
        private IDiveParametersOutputModel _diveParametersModel;
        private IDiveModel _diveModel;
        private IDiveStepModel _diveStep;
        private IGasMixtureModel _gasMixture;

        public PreDiveStageStepInfo(IDiveParametersOutputModel diveParametersModel, IDiveModel diveModel, IDiveStepModel diveStep, IGasMixtureModel gasMixture)
        {
            _diveParametersModel = diveParametersModel;
            _diveModel = diveModel;
            _diveStep = diveStep;
            _gasMixture = gasMixture;

            //_gasManagementController = new GasManagementController();
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

            _diveParametersModel.Depth = _diveStep.Depth;
            _diveParametersModel.Time = _diveStep.Time;

            _diveParametersModel.GasName = _gasMixture.GasName;

            //TODO AH Oxygen, Helium, Nitrogen aren't used here should they be added to the dive parameters used?
        }
    }
}
















