using BubblesDivePlanner.Contracts.Models.DiveModels;
using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Contracts.Models.Results;
using BubblesDivePlanner.Contracts.Services;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.Results;

namespace BubblesDivePlanner.Services
{
    public class DiveProfileService : IDiveProfileService
    {
        private DiveStageHandler _diveStages;
        private IDiveProfile _diveProfile;
        
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

        public DiveProfileService()
        {
            _diveStages = new DiveStageHandler();
            _diveProfile = new DiveProfile();
            _theDiveModel = new DiveModel();
        }

        //TODO AH Strategy pattern on deco model stuff
        public DiveResultsStepOutputModel RunDiveStep(IDiveStepModel diveStep, IGasMixtureModel gasMixture)
        {
            //update internal state
            _diveStages.UpdateDiveStageHandler(TheDiveModel, _diveProfile, diveStep, gasMixture);
            //run all stages
            return _diveStages.RunDiveStages();
        }
        
        public IDiveParametersResultModel UpdateParametersUsed(IDiveStepModel diveStep, IGasMixtureModel selectedGasMixture, IGasManagementModel gasManagementSetupModel)
        {
            return _diveStages.UpdateUsedDiveParameters(diveStep, selectedGasMixture, gasManagementSetupModel);
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

                _diveProfile.AValues.Add(0);
                _diveProfile.BValues.Add(0);
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

            _diveProfile.AValues.Clear();
            _diveProfile.BValues.Clear();
        }
    }
}
