using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;

namespace BubblesDivePlanner.Commands
{
    public class AmbientPressureCommand : IDiveStageCommand
    {
        private readonly IDiveProfile diveProfile;
        private readonly IGasMixture gasMixture;
        private readonly IDiveStep diveStep;

        public AmbientPressureCommand(IDiveProfile diveProfile, IGasMixture gasMixture, IDiveStep diveStep)
        {
            this.diveProfile = diveProfile;
            this.gasMixture = gasMixture;
            this.diveStep = diveStep;
        }

        public void RunDiveStage()
        {
            float pressureAmbient = CalculateAmbientPressure();
            CalculateAdjustedGasPressures(pressureAmbient);
        }

        private float CalculateAmbientPressure()
        {
            return 1.0f + (diveStep.Depth / 10.0f);
        }

        private void CalculateAdjustedGasPressures(float pressureAmbient)
        {
            diveProfile.NitrogenAtPressure = gasMixture.Nitrogen / 100.0f * pressureAmbient;
            diveProfile.OxygenAtPressure = gasMixture.Oxygen / 100.0f * pressureAmbient;
            diveProfile.HeliumAtPressure = gasMixture.Helium / 100.0f * pressureAmbient;
        }
    }
}