using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers.DiveStages
{
    public class DiveStageABValues : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageABValues(IDiveModel diveModel, IDiveProfile diveProfile)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
        }

        public int Compartment 
        {
            get;set;
            }

        //calculates the ab values
        public void RunStage()
        {
            //a and  b coefficients set based on user input
            CalculateAValues();
            CalculateBValues();
        }

        private void CalculateAValues()
        {
            _diveModel.AValues = new List<double>();

            //for (int i = 0; i < _diveModel.AValuesNitrogen.Count; i++)
            //{
            _diveModel.AValues.Add((_diveModel.AValuesNitrogen[Compartment] * _diveProfile.TissuePressuresNitrogen[Compartment] + _diveModel.AValuesHelium[Compartment] * _diveProfile.TissuePressuresHelium[Compartment]) / _diveProfile.TissuePressuresTotal[Compartment]);
            //}
        }

        private void CalculateBValues()
        {
            _diveModel.BValues = new List<double>();

            //for (int i = 0; i < _diveModel.BValuesNitrogen.Count; i++)
            //{
            _diveModel.BValues.Add((_diveModel.BValuesNitrogen[Compartment] * _diveProfile.TissuePressuresNitrogen[Compartment] + _diveModel.BValuesHelium[Compartment] * _diveProfile.TissuePressuresHelium[Compartment]) / _diveProfile.TissuePressuresTotal[Compartment]);
            //}
        }
    }
}