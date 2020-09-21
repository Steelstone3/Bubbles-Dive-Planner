using System;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageTissuePressure : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;
        private int _bottomTime;

        public DiveStageTissuePressure(IDiveModel diveModel, IDiveProfile diveProfile, int bottomTime)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
            _bottomTime = bottomTime;
        }

        public void RunStage()
        {
            CalculateTissuePressureNitrogen();
            CalculateTissuePressureHelium();
            CalculateTotalTissuesPressure();
        }

        private void CalculateTissuePressureNitrogen()
        {
            foreach (int tissuePressureNitrogen in _diveProfile.TissuePressuresNitrogen)
            {
                //works out tissue pressure for a given compartment (Nitrogen)
                _diveProfile.TissuePressuresNitrogen[tissuePressureNitrogen] = _diveProfile.TissuePressuresNitrogen[tissuePressureNitrogen] + ((_diveProfile.PressureNitrogen - _diveProfile.TissuePressuresNitrogen[tissuePressureNitrogen]) * (1.0f - Math.Pow(2.0f, -(_bottomTime / _diveModel.NitrogenHalfTime[tissuePressureNitrogen]))));
            }
        }

        private void CalculateTissuePressureHelium()
        {
            foreach (int tissuePressureHelium in _diveProfile.TissuePressuresHelium)
            {
                //works out tissue pressure for a given compartment (Helium)
                _diveProfile.TissuePressuresHelium[tissuePressureHelium] = _diveProfile.TissuePressuresHelium[tissuePressureHelium] + ((_diveProfile.PressureHelium - _diveProfile.TissuePressuresHelium[tissuePressureHelium]) * (1.0f - Math.Pow(2.0f, -(_bottomTime / _diveModel.HeliumHalfTime[tissuePressureHelium]))));
            }
        }

        private void CalculateTotalTissuesPressure()
        {
            foreach (int tissuePressure in _diveProfile.TissuePressuresNitrogen)
            {
                //total combined tissue pressure
                _diveProfile.TissuePressuresTotal[tissuePressure] = _diveProfile.TissuePressuresHelium[tissuePressure] + _diveProfile.TissuePressuresNitrogen[tissuePressure];
            }
        }
    }
}