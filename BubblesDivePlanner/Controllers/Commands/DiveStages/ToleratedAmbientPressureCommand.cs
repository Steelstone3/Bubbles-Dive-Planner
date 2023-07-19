using System;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;

namespace BubblesDivePlanner.Commands
{
    public class ToleratedAmbientPressureCommand : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;

        public ToleratedAmbientPressureCommand(IDiveModel diveModel)
        {
            this.diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.Compartments; compartment++)
            {
                CalculateToleratedAmbientPressure(compartment);
            }
        }

        private void CalculateToleratedAmbientPressure(int compartment)
        {
            diveModel.DiveProfile.ToleratedAmbientPressures[compartment] = (float)Math.Round((diveModel.DiveProfile.TotalTissuePressures[compartment] - diveModel.DiveProfile.AValues[compartment]) * diveModel.DiveProfile.BValues[compartment], 4);
        }
    }
}