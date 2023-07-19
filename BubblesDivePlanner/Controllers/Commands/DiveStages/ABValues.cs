using System;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;

namespace BubblesDivePlanner.Commands
{
    public class ABValues : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;

        public ABValues(IDiveModel diveModel)
        {
            this.diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.Compartments; compartment++)
            {
                CalculateAValues(compartment);
                CalculateBValues(compartment);
            }
        }

        private void CalculateAValues(int compartment)
        {
            diveModel.DiveProfile.AValues[compartment] = (float)Math.Round(((diveModel.AValuesNitrogen[compartment] * diveModel.DiveProfile.NitrogenTissuePressures[compartment]) + (diveModel.AValuesHelium[compartment] * diveModel.DiveProfile.HeliumTissuePressures[compartment])) / diveModel.DiveProfile.TotalTissuePressures[compartment], 4);
        }

        private void CalculateBValues(int compartment)
        {
            diveModel.DiveProfile.BValues[compartment] = (float)Math.Round(((diveModel.BValuesNitrogen[compartment] * diveModel.DiveProfile.NitrogenTissuePressures[compartment]) + (diveModel.BValuesHelium[compartment] * diveModel.DiveProfile.HeliumTissuePressures[compartment])) / diveModel.DiveProfile.TotalTissuePressures[compartment], 4);
        }
    }
}