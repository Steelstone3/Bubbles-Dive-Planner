using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageAmbientPressure : IDiveStage
    {
        private IDiveProfile _diveProfile;
        private double _oxygenPercentage;
        private double _heliumPercentage;
        private int _depth;

        public DiveStageAmbientPressure(IDiveProfile diveProfile, double oxygenPercentage, double heliumPercentage, int depth)
        {
            _diveProfile = diveProfile;
            _oxygenPercentage = oxygenPercentage;
            _heliumPercentage = heliumPercentage;
            _depth = depth;
        }

        public void RunStage()
        {
            //calculates tolerated ambient pressure of diver based on user inputs
            CalculateAmbientPressure();
        }

        private void CalculateAmbientPressure()
        {
            //taken from user input used to calculate the pressure at depth for nitrogen
            //calcs nitrogen pressure being breathed
            var nitrogenFraction = (1.0 - (_oxygenPercentage / 100 + _heliumPercentage / 100));

            //calculates ambient pressure
            var pressureAmbient = (1.0 + (double)_depth / 10.0);

            //calculates ambient pressure of each gas
            _diveProfile.PressureNitrogen = nitrogenFraction * pressureAmbient;
            _diveProfile.PressureOxygen = (_oxygenPercentage / 100 * pressureAmbient);
            _diveProfile.PressureHelium = (_heliumPercentage / 100 * pressureAmbient);
        }
    }
}