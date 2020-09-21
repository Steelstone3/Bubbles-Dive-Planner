using System.Collections.Generic;
using System.Threading.Tasks;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageABValues : IDiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public List<double> AValues
        {
            get; private set;
        } = new List<double>();

        public List<double> BValues
        {
            get; private set;
        } = new List<double>();


        public DiveStageABValues(IDiveModel diveModel, IDiveProfile diveProfile)
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
        }

        //calculates the ab values
        //public async void RunStage()
        public void RunStage()
        {
            //a and  b coefficients set based on user input
            //await CalculateAValues();
            //await CalculateBValues();
            CalculateAValues();
            CalculateBValues();

        } 

        //private Task CalculateAValues()
        private void CalculateAValues()
        {
            foreach (int aValueCompartment in _diveModel.AValuesNitrogen)
            {
                var aValue = (_diveModel.AValuesNitrogen[aValueCompartment] * _diveProfile.TissuePressuresNitrogen[aValueCompartment] + _diveModel.AValuesHelium[aValueCompartment] * _diveProfile.TissuePressuresHelium[aValueCompartment]) / _diveProfile.TissuePressuresTotal[aValueCompartment];
                AValues[aValueCompartment] = aValue;
            }
        }

        //private Task CalculateBValues()
        private void CalculateBValues()
        {
            foreach (int bValueCompartment in _diveModel.BValuesNitrogen)
            {
                var bValue = (_diveModel.BValuesNitrogen[bValueCompartment] * _diveProfile.TissuePressuresNitrogen[bValueCompartment] + _diveModel.BValuesHelium[bValueCompartment] * _diveProfile.TissuePressuresHelium[bValueCompartment]) / _diveProfile.TissuePressuresTotal[bValueCompartment];
                BValues[bValueCompartment] = bValue;
            }
        }
    }
}