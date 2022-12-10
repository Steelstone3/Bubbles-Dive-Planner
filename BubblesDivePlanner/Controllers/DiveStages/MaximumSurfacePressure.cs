using System;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class MaximumSurfacePressure : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;

        public MaximumSurfacePressure(IDiveModel diveModel)
        {
            this.diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
            {
                CalculateMaximumSurfacePressure(compartment);
            }
        }

        private void CalculateMaximumSurfacePressure(int compartment)
        {
            diveModel.DiveProfile.MaxSurfacePressures[compartment] = Math.Round(1.0f / diveModel.DiveProfile.BValues[compartment] + diveModel.DiveProfile.AValues[compartment], 4);
        }
    }
}