using System.Threading.Tasks;
using DivePlannerMk3.Contracts;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageABValues : IDiveStage
    {
        private IDiveModel _theDiveModel;
        private IDiveProfile _diveProfile;
        private int _compartment;

        public DiveStageABValues(int compartment, IDiveModel theDiveModel, IDiveProfile diveProfile)
        {
            _theDiveModel = theDiveModel;
            _diveProfile = diveProfile;
            _compartment = compartment;
        }

        //calculates the ab values
        public void RunStage()
        {
            //a and  b coefficients set based on user input
            var aValues = ( ( _theDiveModel.AValuesNitrogen[_compartment] * _diveProfile.TissuePressuresNitrogen[_compartment] ) + ( _theDiveModel.AValuesHelium[_compartment] * _diveProfile.TissuePressuresHelium[_compartment] ) ) / _diveProfile.TissuePressuresTotal[_compartment];
            var bValues = ( ( _theDiveModel.BValuesNitrogen[_compartment] * _diveProfile.TissuePressuresNitrogen[_compartment] ) + ( _theDiveModel.BValuesHelium[_compartment] * _diveProfile.TissuePressuresHelium[_compartment] ) ) / _diveProfile.TissuePressuresTotal[_compartment];
            
        }
    }
}