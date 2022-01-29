using System;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.DiveStages
{
    public class TissuePressureCommand : IDiveStageCommand
    {
        private IDiveModel _diveModel;
        private IDiveStepModel _diveStepModel;

        public TissuePressureCommand(IDiveModel diveModel, IDiveStepModel diveStepModel)
        {
            _diveModel = diveModel;
            _diveStepModel = diveStepModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < _diveModel.CompartmentCount; compartment++)
            {
                CalculateTissuePressureNitrogen(compartment);
                CalculateTissuePressureHelium(compartment);
                CalculateTotalTissuesPressure(compartment);
            }
        }

        private void CalculateTissuePressureNitrogen(int compartment)
        {
            _diveModel.DiveProfile.TissuePressuresNitrogen[compartment] = _diveModel.DiveProfile.TissuePressuresNitrogen[compartment] + 
                                                                        ((_diveModel.DiveProfile.PressureNitrogen - 
                                                                        _diveModel.DiveProfile.TissuePressuresNitrogen[compartment]) * 
                                                                        (1.0f - Math.Pow(2.0f, -(_diveStepModel.Time / _diveModel.NitrogenHalfTime[compartment]))));
        }

        private void CalculateTissuePressureHelium(int compartment)
        {
            _diveModel.DiveProfile.TissuePressuresHelium[compartment] = _diveModel.DiveProfile.TissuePressuresHelium[compartment] + 
                                                                        ((_diveModel.DiveProfile.PressureHelium - 
                                                                        _diveModel.DiveProfile.TissuePressuresHelium[compartment]) *
                                                                        (1.0f - Math.Pow(2.0f, - (_diveStepModel.Time / _diveModel.HeliumHalfTime[compartment]))));
        }

        private void CalculateTotalTissuesPressure(int compartment)
        {
            _diveModel.DiveProfile.TissuePressuresTotal[compartment] = _diveModel.DiveProfile.TissuePressuresHelium[compartment] +
                                                                       _diveModel.DiveProfile.TissuePressuresNitrogen[compartment];
        }
    }
}