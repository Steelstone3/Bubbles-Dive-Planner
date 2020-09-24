using System;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class DiveStageTissuePressure : DiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;
        private int _bottomTime;

        public DiveStageTissuePressure(IDiveModel diveModel, IDiveProfile diveProfile, int bottomTime) : base()
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
            _bottomTime = bottomTime;
        }

        public override void RunStage()
        {
            CalculateTissuePressureNitrogen();
            CalculateTissuePressureHelium();
            CalculateTotalTissuesPressure();
            CompartmentCountCheck(_diveModel.CompartmentCount - 1);
        }

        private void CalculateTissuePressureNitrogen()
        {
            //works out tissue pressure for a given compartment (Nitrogen)
            _diveProfile.TissuePressuresNitrogen[Compartment] = _diveProfile.TissuePressuresNitrogen[Compartment] + ((_diveProfile.PressureNitrogen - _diveProfile.TissuePressuresNitrogen[Compartment]) * (1.0f - Math.Pow(2.0f, -(_bottomTime / _diveModel.NitrogenHalfTime[Compartment]))));
        }

        private void CalculateTissuePressureHelium()
        {
            //works out tissue pressure for a given compartment (Helium)
            _diveProfile.TissuePressuresHelium[Compartment] = _diveProfile.TissuePressuresHelium[Compartment] + ((_diveProfile.PressureHelium - _diveProfile.TissuePressuresHelium[Compartment]) * (1.0f - Math.Pow(2.0f, -(_bottomTime / _diveModel.HeliumHalfTime[Compartment]))));
        }

        private void CalculateTotalTissuesPressure()
        {
            //total combined tissue pressure
            _diveProfile.TissuePressuresTotal[Compartment] = _diveProfile.TissuePressuresHelium[Compartment] + _diveProfile.TissuePressuresNitrogen[Compartment];
        }
    }
}