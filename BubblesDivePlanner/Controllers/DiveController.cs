using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public class DiveController : IDiveController
    {
        private readonly IDiveStagesController diveStagesController;

        public DiveController(IDiveStagesController diveStagesController)
        {
            this.diveStagesController = diveStagesController;
        }

        public IDivePlan RunDiveProfile(IDivePlan divePlan)
        {
            return diveStagesController.Run(divePlan);
        }
    }
}