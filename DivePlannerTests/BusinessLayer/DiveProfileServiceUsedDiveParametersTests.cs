using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers.DiveStages;
using DivePlannerMk3.Models;
using Xunit;

namespace DivePlannerTests
{
    public class DiveProfileServiceUsedDiveParametersTests
    {
        //TODO AH need to test are all parameters are assigned through the Pre Dive Stage outputed to this type IDiveParametersOutputModel

        private IDiveParametersOutputModel _diveParametersModel = new DiveParametersOutputModel();
        private IDiveModel _diveModel = new Zhl16Buhlmann();
        private IDiveStepModel _diveStep = new DiveStepModel();
        private IGasMixtureModel _gasMixture = new GasMixtureModel();
        private IGasManagementModel _gasManagement = new GasManagementModel();

        [Fact(Skip="On the to do of tests")]
        public void Test()
        {
            var _preDiveStageStepInfo = new PreDiveStageStepInfo(_diveParametersModel,_diveModel,_diveStep,_gasMixture, _gasManagement);
        }
    }
}