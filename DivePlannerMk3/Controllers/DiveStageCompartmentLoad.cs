using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageCompartmentLoad : IDiveStage
    {
        private IDiveProfileStepOutputModel _result;
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageCompartmentLoad(IDiveProfileStepOutputModel result, IDiveModel diveModel, IDiveProfile diveProfile)
        {
            _result = result;
             _diveModel = diveModel;
             _diveProfile = diveProfile;
        }

        public void RunStage()
        {
           CalculateCompartmentLoad();
        }

        private void CalculateCompartmentLoad()
        {
            for (int i = 0; i < _diveProfile.TissuePressuresTotal.Count; i++)
            {
                //TODO AH wont produce all the results
                _result.CompartmentLoadResult = _diveProfile.CompartmentLoad[i] = _diveProfile.TissuePressuresTotal[i] / _diveProfile.MaxSurfacePressures[i] * 100;
            }
        }
    }
}