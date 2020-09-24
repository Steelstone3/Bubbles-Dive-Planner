using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class DiveStageABValues : DiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageABValues(IDiveModel diveModel, IDiveProfile diveProfile) : base()
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
        }

        //calculates the ab values
        public override void RunStage()
        {
            //a and  b coefficients set based on user input
            CalculateAValues();
            CalculateBValues();
            CompartmentCountCheck(_diveModel.CompartmentCount - 1);
        }

        private void CalculateAValues()
        {
            _diveProfile.AValues.Add((_diveModel.AValuesNitrogen[Compartment] * _diveProfile.TissuePressuresNitrogen[Compartment] + _diveModel.AValuesHelium[Compartment] * _diveProfile.TissuePressuresHelium[Compartment]) / _diveProfile.TissuePressuresTotal[Compartment]);
        }

        private void CalculateBValues()
        {
            _diveProfile.BValues.Add((_diveModel.BValuesNitrogen[Compartment] * _diveProfile.TissuePressuresNitrogen[Compartment] + _diveModel.BValuesHelium[Compartment] * _diveProfile.TissuePressuresHelium[Compartment]) / _diveProfile.TissuePressuresTotal[Compartment]);
        }
    }
}