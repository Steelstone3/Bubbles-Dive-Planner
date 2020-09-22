using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class DiveStageCompartmentLoad : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageCompartmentLoad(IDiveModel diveModel, IDiveProfile diveProfile)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
        }

        public int Compartment 
        {
             get;set;
             }
             
        public void RunStage()
        {
            CalculateCompartmentLoad();
        }

        private void CalculateCompartmentLoad()
        {
            //for (int i = 0; i < _diveProfile.TissuePressuresTotal.Count; i++)
            //{
            //TODO AH wont produce all the results
            _diveProfile.CompartmentLoad[Compartment] = _diveProfile.TissuePressuresTotal[Compartment] / _diveProfile.MaxSurfacePressures[Compartment] * 100;
            //}
        }
    }
}