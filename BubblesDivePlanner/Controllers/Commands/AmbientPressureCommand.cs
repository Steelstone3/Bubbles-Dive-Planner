using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Commands
{
    public class AmbientPressureCommand : IDiveStageCommand
    {
        private readonly IDiveProfileModel _diveProfileModel;
        private readonly IGasMixtureModel _gasMixtureModel;
        private readonly IDiveStepModel _diveStepModel;

        public AmbientPressureCommand(IDiveProfileModel diveProfile, IGasMixtureModel gasMixtureModel, IDiveStepModel diveStepModel)
        {
            _diveProfileModel = diveProfile;
            _gasMixtureModel = gasMixtureModel;
            _diveStepModel = diveStepModel;
        }

        public void RunDiveStage()
        {
            var pressureAmbient = CalculateAmbientPressure();
            CalculateAdjustedGasPressures(pressureAmbient);
        }

        private double CalculateAmbientPressure()
        {
            return 1.0 + (_diveStepModel.Depth / 10.0);
        }

        private void CalculateAdjustedGasPressures(double pressureAmbient)
        {
            _diveProfileModel.PressureNitrogen = _gasMixtureModel.Nitrogen / 100 * pressureAmbient;
            _diveProfileModel.PressureOxygen = _gasMixtureModel.Oxygen / 100 * pressureAmbient;
            _diveProfileModel.PressureHelium = _gasMixtureModel.Helium / 100 * pressureAmbient;
        }
    }
}