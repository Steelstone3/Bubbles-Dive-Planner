using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageABValues : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageABValues(IDiveModel diveModel, IDiveProfile diveProfile)
        {
            //TODO AH dive model is null
            _diveModel = diveModel;
            _diveProfile = diveProfile;
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

            for (int i = 0; i < _diveModel.AValuesNitrogen.Count; i++)
            {
                _diveModel.AValues.Add((_diveModel.AValuesNitrogen[i] * _diveProfile.TissuePressuresNitrogen[i] + _diveModel.AValuesHelium[i] * _diveProfile.TissuePressuresHelium[i]) / _diveProfile.TissuePressuresTotal[i]);
            }
        }

        private void CalculateBValues()
        {
            _diveModel.BValues = new List<double>();

            for (int i = 0; i < _diveModel.BValuesNitrogen.Count; i++)
            {
                _diveModel.BValues.Add((_diveModel.BValuesNitrogen[i] * _diveProfile.TissuePressuresNitrogen[i] + _diveModel.BValuesHelium[i] * _diveProfile.TissuePressuresHelium[i]) / _diveProfile.TissuePressuresTotal[i]);
            }
        }
    }
}