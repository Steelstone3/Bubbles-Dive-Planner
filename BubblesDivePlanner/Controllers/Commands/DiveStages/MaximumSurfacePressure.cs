using System;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;

namespace BubblesDivePlanner.Commands
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
            for (int compartment = 0; compartment < diveModel.Compartments; compartment++)
            {
                CalculateMaximumSurfacePressure(compartment);
            }
        }

        private void CalculateMaximumSurfacePressure(int compartment)
        {
            diveModel.DiveProfile.MaxSurfacePressures[compartment] = (float)Math.Round((1.0f / diveModel.DiveProfile.BValues[compartment]) + diveModel.DiveProfile.AValues[compartment], 4);
        }
    }
}