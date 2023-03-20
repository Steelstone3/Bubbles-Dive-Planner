using System;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.DiveModels;

namespace BubblesDivePlanner.Commands
{
    public class AbValuesCommand : IDiveStageCommand
    {
        private readonly IDiveModel _diveModel;

        public AbValuesCommand(IDiveModel diveModel)
        {
            _diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < _diveModel.CompartmentCount; compartment++)
            {
                CalculateAValues(compartment);
                CalculateBValues(compartment);
            }
        }

        private void CalculateAValues(int compartment)
        {
            _diveModel.DiveProfile.AValues[compartment] = Math.Round((_diveModel.AValuesNitrogen[compartment] * _diveModel.DiveProfile.TissuePressuresNitrogen[compartment] + _diveModel.AValuesHelium[compartment] * _diveModel.DiveProfile.TissuePressuresHelium[compartment]) / _diveModel.DiveProfile.TissuePressuresTotal[compartment], 4);
        }

        private void CalculateBValues(int compartment)
        {
            _diveModel.DiveProfile.BValues[compartment] = Math.Round((_diveModel.BValuesNitrogen[compartment] * _diveModel.DiveProfile.TissuePressuresNitrogen[compartment] + _diveModel.BValuesHelium[compartment] * _diveModel.DiveProfile.TissuePressuresHelium[compartment]) / _diveModel.DiveProfile.TissuePressuresTotal[compartment], 4);
        }
    }
}