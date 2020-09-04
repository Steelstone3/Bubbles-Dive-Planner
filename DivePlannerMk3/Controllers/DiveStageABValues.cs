using System.Threading.Tasks;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageABValues : IDiveStage
    {
        private IDiveModel _theDiveModel;
        private IDiveProfile _diveProfile;

        public DiveStageABValues(IDiveModel theDiveModel, IDiveProfile diveProfile)
        {
            _theDiveModel = theDiveModel;
            _diveProfile = diveProfile;
        }

        //calculates the ab values
        public void RunStage()
        {
            //a and  b coefficients set based on user input
            var aValues = ( ( _theDiveModel.AValuesNitrogen[compartment] * _diveProfile.TissuePressuresNitrogen[compartment] ) + ( _theDiveModel.AValuesHelium[compartment] * _diveProfile.TissuePressuresHelium[compartment] ) ) / _diveProfile.TissuePressuresTotal[compartment];
            var bValues = ( ( _theDiveModel.BValuesNitrogen[compartment] * _diveProfile.TissuePressuresNitrogen[compartment] ) + ( _theDiveModel.BValuesHelium[compartment] * _diveProfile.TissuePressuresHelium[compartment] ) ) / _diveProfile.TissuePressuresTotal[compartment];
            
        }
    }
}