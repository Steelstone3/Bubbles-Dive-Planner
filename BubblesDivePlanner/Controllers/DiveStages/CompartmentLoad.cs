using System;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class CompartmentLoad : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;

        public CompartmentLoad(IDiveModel diveModel)
        {
            this.diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
            {
                CalculateCompartmentLoad(compartment);
            }
        }

        private void CalculateCompartmentLoad(int compartment)
        {
            diveModel.DiveProfile.CompartmentLoads[compartment] = Math.Round(diveModel.DiveProfile.TotalTissuePressures[compartment] / diveModel.DiveProfile.MaxSurfacePressures[compartment] * 100, 2);
        }
    }
}