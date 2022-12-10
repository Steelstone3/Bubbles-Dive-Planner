using System;
using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.DiveStages
{
    public class ToleratedAmbientPressure : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;

        public ToleratedAmbientPressure(IDiveModel diveModel)
        {
            this.diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
            {
                CalculateToleratedAmbientPressure(compartment);
            }
        }

        private void CalculateToleratedAmbientPressure(int compartment)
        {
            diveModel.DiveProfile.ToleratedAmbientPressures[compartment] = Math.Round((diveModel.DiveProfile.TotalTissuePressures[compartment] - diveModel.DiveProfile.AValues[compartment]) * diveModel.DiveProfile.BValues[compartment], 4);
        }
    }
}