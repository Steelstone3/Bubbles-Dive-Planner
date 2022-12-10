using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class GasManagement : IDiveStageCommand
    {
        private readonly ICylinder selectedCylinder;
        private readonly IDiveStep diveStep;

        public GasManagement(ICylinder selectedCylinder, IDiveStep diveStep)
        {
            this.selectedCylinder = selectedCylinder;
            this.diveStep = diveStep;
        }

        public void RunDiveStage()
        {
            selectedCylinder.UpdateCylinderGasConsumption(diveStep);
        }
    }
}