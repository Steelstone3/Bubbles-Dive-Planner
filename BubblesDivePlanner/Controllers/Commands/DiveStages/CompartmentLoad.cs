using System;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;

namespace BubblesDivePlanner.Commands
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
            for (int compartment = 0; compartment < diveModel.Compartments; compartment++)
            {
                CalculateCompartmentLoad(compartment);
            }
        }

        private void CalculateCompartmentLoad(int compartment)
        {
            diveModel.DiveProfile.CompartmentLoads[compartment] = (float)Math.Round(diveModel.DiveProfile.TotalTissuePressures[compartment] / diveModel.DiveProfile.MaxSurfacePressures[compartment] * 100, 2);
        }
    }
}