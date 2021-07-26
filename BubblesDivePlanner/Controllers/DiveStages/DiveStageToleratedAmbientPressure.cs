using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class DiveStageToleratedAmbientPressure : DiveStage
    {
        private int _compartmentCount;
        private IDiveProfile _diveProfile;

        public DiveStageToleratedAmbientPressure(int compartmentCount, IDiveProfile diveProfile) : base()
        {
            _compartmentCount = compartmentCount;
            _diveProfile = diveProfile;
        }

        public override void RunStage()
        {
            CalculateToleratedAmbientPressure();
            CompartmentCountCheck(_compartmentCount - 1);
        }

        private void CalculateToleratedAmbientPressure()
        {
            _diveProfile.ToleratedAmbientPressures[Compartment] = (_diveProfile.TissuePressuresTotal[Compartment] - _diveProfile.AValues[Compartment]) * _diveProfile.BValues[Compartment];
        }
    }
}