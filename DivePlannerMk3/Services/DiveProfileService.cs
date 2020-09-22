using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using DivePlannerMk3.Services;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveProfileService : IDiveProfileService
    {
        private IDiveProfile _diveProfile = new DiveProfile();

        private IDiveModel _theDiveModel;
        public IDiveModel TheDiveModel
        {
            get => _theDiveModel;
            set
            {
                _theDiveModel = value;
                InitaliseDiveProfile();
            }
        }

        //TODO AH Strategy pattern
        public DiveProfileResultsListViewModel RunDiveStep(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            var diveStages = new DiveStageHandler(TheDiveModel, _diveProfile, diveStep, gasMixture);
            var outputResults = new DiveProfileResultsListViewModel();

           return diveStages.RunAllDiveStages();
        }

        private void InitaliseDiveProfile()
        {
            ResetDiveProfile();

            for (int i = 0; i < TheDiveModel.CompartmentCount; i++)
            {
                _diveProfile.MaxSurfacePressures.Add(0);
                _diveProfile.ToleratedAmbientPressures.Add(0);
                _diveProfile.CompartmentLoad.Add(0);

                _diveProfile.TissuePressuresNitrogen.Add(0.79);
                _diveProfile.TissuePressuresHelium.Add(0);
                _diveProfile.TissuePressuresTotal.Add(0);
            }

        }

        private void ResetDiveProfile()
        {
            _diveProfile.MaxSurfacePressures.Clear();
            _diveProfile.ToleratedAmbientPressures.Clear();
            _diveProfile.CompartmentLoad.Clear();

            _diveProfile.TissuePressuresNitrogen.Clear();
            _diveProfile.TissuePressuresHelium.Clear();
            _diveProfile.TissuePressuresTotal.Clear();
        }
    }
}
