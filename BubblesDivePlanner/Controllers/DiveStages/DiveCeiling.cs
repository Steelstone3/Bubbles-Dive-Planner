using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class DiveCeiling : IDiveStageCommand
    {
        private IDiveProfile diveProfile;

        public DiveCeiling(IDiveProfile diveProfile)
        {
            this.diveProfile = diveProfile;
        }

        public void RunDiveStage()
        {
            diveProfile.UpdateDepthCeiling();
        }
    }
}