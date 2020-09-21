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
            //TODO AH dive model null
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
            for (int i = 0; i < _diveProfile.TissuePressuresNitrogen.Count; i++)
            {
                //works out tissue pressure for a given compartment (Nitrogen)
                _diveProfile.TissuePressuresNitrogen[i] = _diveProfile.TissuePressuresNitrogen[i] + ((_diveProfile.PressureNitrogen - _diveProfile.TissuePressuresNitrogen[i]) * (1.0f - Math.Pow(2.0f, -(_bottomTime / _diveModel.NitrogenHalfTime[i]))));
            }
        }

        private void CalculateTissuePressureHelium()
        {
            for (int i = 0; i < _diveProfile.TissuePressuresHelium.Count; i++)
            {
                //works out tissue pressure for a given compartment (Helium)
                _diveProfile.TissuePressuresHelium[i] = _diveProfile.TissuePressuresHelium[i] + ((_diveProfile.PressureHelium - _diveProfile.TissuePressuresHelium[i]) * (1.0f - Math.Pow(2.0f, -(_bottomTime / _diveModel.HeliumHalfTime[i]))));
            }
        }

        private void CalculateTotalTissuesPressure()
        {
            for (int i = 0; i < _diveProfile.TissuePressuresNitrogen.Count; i++)
            {
                //total combined tissue pressure
                _diveProfile.TissuePressuresTotal[i] = _diveProfile.TissuePressuresHelium[i] + _diveProfile.TissuePressuresNitrogen[i];
            }
        }
    }
}