using DivePlannerMK3.Contracts;
using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageMaximumSurfacePressure : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageMaximumSurfacePressure(IDiveModel diveModel, IDiveProfile diveProfile)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
            
        }

        public void RunStage()
        {
            CalculateMaximumSurfacePressure();
        }

        private void CalculateMaximumSurfacePressure()
        {
            for (int i = 0; i < _diveModel.AValues.Count; i++)
            {
                _diveProfile.MaxSurfacePressures[i] = (1.0f / _diveModel.BValues[i]) + _diveModel.AValues[i];
            }
        }
    }
}