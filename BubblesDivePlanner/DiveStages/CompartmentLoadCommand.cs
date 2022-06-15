using System;
using BubblesDivePlanner.DiveModels;

namespace BubblesDivePlanner.DiveStages
{
    public class CompartmentLoadCommand : IDiveStageCommand
    {
        private readonly IDiveModel _diveModel;

        public CompartmentLoadCommand(IDiveModel diveModel)
        {
            _diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < _diveModel.CompartmentCount; compartment++)
            {
                CalculateCompartmentLoad(compartment);
            }
        }

        private void CalculateCompartmentLoad(int compartment)
        {
            _diveModel.DiveProfile.CompartmentLoad[compartment] = Math.Round(_diveModel.DiveProfile.TissuePressuresTotal[compartment] / _diveModel.DiveProfile.MaxSurfacePressures[compartment] * 100, 2);
        }
    }
}