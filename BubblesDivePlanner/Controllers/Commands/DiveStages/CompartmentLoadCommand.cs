using System;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;

namespace BubblesDivePlanner.Commands
{
    public class CompartmentLoadCommand : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;

        public CompartmentLoadCommand(IDiveModel diveModel)
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