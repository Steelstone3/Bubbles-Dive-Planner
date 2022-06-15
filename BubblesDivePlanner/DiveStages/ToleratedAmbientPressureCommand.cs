using System;
using BubblesDivePlanner.DiveModels;

namespace BubblesDivePlanner.DiveStages
{
    public class ToleratedAmbientPressureCommand : IDiveStageCommand
    {
        private readonly IDiveModel _diveModel;

        public ToleratedAmbientPressureCommand(IDiveModel diveModel)
        {
            _diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < _diveModel.CompartmentCount; compartment++)
            {
                CalculateToleratedAmbientPressure(compartment);
            }
        }

        private void CalculateToleratedAmbientPressure(int compartment)
        {
            _diveModel.DiveProfile.ToleratedAmbientPressures[compartment] = Math.Round((_diveModel.DiveProfile.TissuePressuresTotal[compartment] - _diveModel.DiveProfile.AValues[compartment]) * _diveModel.DiveProfile.BValues[compartment], 4);
        }
    }
}