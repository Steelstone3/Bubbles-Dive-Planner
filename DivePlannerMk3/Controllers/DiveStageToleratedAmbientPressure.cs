using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageToleratedAmbientPressure : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageToleratedAmbientPressure(IDiveModel diveModel, IDiveProfile diveProfile)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
        }

        public void RunStage()
        {
            CalculateToleratedAmbientPressure();
        }

        private void CalculateToleratedAmbientPressure()
        {
            for (int i = 0; i < _diveProfile.TissuePressuresTotal.Count; i++)
            {
                _diveProfile.ToleratedAmbientPressures[i] = (_diveProfile.TissuePressuresTotal[i] - _diveModel.AValues[i]) * _diveModel.BValues[i];
            }
        }
    }
}