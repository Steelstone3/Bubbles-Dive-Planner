using BubblesDivePlanner.Contracts.Models.DiveModels;

namespace BubblesDivePlanner.Commands.DiveStages
{
    public class DiveStageCompartmentLoad : DiveStage
    {
        private IDiveModel _diveModel;
        private IDiveProfile _diveProfile;

        public DiveStageCompartmentLoad(IDiveModel diveModel, IDiveProfile diveProfile) : base()
        {
            _diveModel = diveModel;
            _diveProfile = diveProfile;
        }

        public override void RunStage()
        {
            CalculateCompartmentLoad();
            CompartmentCountCheck(_diveModel.CompartmentCount - 1);
        }

        private void CalculateCompartmentLoad()
        {
            _diveProfile.CompartmentLoad[Compartment] = _diveProfile.TissuePressuresTotal[Compartment] / _diveProfile.MaxSurfacePressures[Compartment] * 100;
        }
    }
}