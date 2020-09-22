using DivePlannerMK3.Contracts;
using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class DiveStageMaximumSurfacePressure : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;
        
        public int Compartment 
        {
            get;set;
        }
        
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
            //for (int i = 0; i < _diveModel.AValues.Count; i++)
            //{
            //TODO AH wont produce all the results
            _diveProfile.MaxSurfacePressures[Compartment] = (1.0f / _diveModel.BValues[Compartment]) + _diveModel.AValues[Compartment];
            //}
        }
    }
}