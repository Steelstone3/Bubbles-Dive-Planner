using System;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class AbValues : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;

        public AbValues(IDiveModel diveModel)
        {
            this.diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
            {
                CalculateAValues(compartment);
                CalculateBValues(compartment);
            }
        }

        private void CalculateAValues(int compartment)
        {
            diveModel.DiveProfile.AValues[compartment] = Math.Round((diveModel.AValuesNitrogen[compartment] * diveModel.DiveProfile.NitrogenTissuePressures[compartment] + diveModel.AValuesHelium[compartment] * diveModel.DiveProfile.HeliumTissuePressures[compartment]) / diveModel.DiveProfile.TotalTissuePressures[compartment], 4);
        }

        private void CalculateBValues(int compartment)
        {
            diveModel.DiveProfile.BValues[compartment] = Math.Round((diveModel.BValuesNitrogen[compartment] * diveModel.DiveProfile.NitrogenTissuePressures[compartment] + diveModel.BValuesHelium[compartment] * diveModel.DiveProfile.HeliumTissuePressures[compartment]) / diveModel.DiveProfile.TotalTissuePressures[compartment], 4);
        }
    }
}