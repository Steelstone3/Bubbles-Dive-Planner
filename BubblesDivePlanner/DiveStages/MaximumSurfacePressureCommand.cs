using System;
using BubblesDivePlanner.DiveModels;

namespace BubblesDivePlanner.DiveStages
{
    public class MaximumSurfacePressureCommand : IDiveStageCommand
    {
        private IDiveModel _diveModel;

        public MaximumSurfacePressureCommand(IDiveModel diveModel)
        {
            _diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < _diveModel.CompartmentCount; compartment++)
            {                
                CalculateMaximumSurfacePressure(compartment);
            }
        }

        private void CalculateMaximumSurfacePressure(int compartment)
        {
            _diveModel.DiveProfile.MaxSurfacePressures[compartment] = Math.Round((1.0f / _diveModel.DiveProfile.BValues[compartment]) + _diveModel.DiveProfile.AValues[compartment], 4);
        }
    }
}