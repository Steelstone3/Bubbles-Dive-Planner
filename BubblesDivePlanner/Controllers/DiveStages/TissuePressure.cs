using System;
using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.DiveStages
{
    public class TissuePressure : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;
        private readonly IDiveStep diveStep;

        public TissuePressure(IDiveModel diveModel, IDiveStep diveStepModel)
        {
            this.diveModel = diveModel;
            diveStep = diveStepModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
            {
                CalculateTissuePressureNitrogen(compartment);
                CalculateTissuePressureHelium(compartment);
                CalculateTotalTissuesPressure(compartment);
            }
        }

        private void CalculateTissuePressureNitrogen(int compartment)
        {
            diveModel.DiveProfile.NitrogenTissuePressures[compartment] = Math.Round(diveModel.DiveProfile.NitrogenTissuePressures[compartment] +
                                                                        ((diveModel.DiveProfile.NitrogenPressureAtDepth -
                                                                        diveModel.DiveProfile.NitrogenTissuePressures[compartment]) *
                                                                        (1.0f - Math.Pow(2.0f, -(diveStep.Time / diveModel.NitrogenHalfTimes[compartment])))), 4);
        }

        private void CalculateTissuePressureHelium(int compartment)
        {
            diveModel.DiveProfile.HeliumTissuePressures[compartment] = Math.Round(diveModel.DiveProfile.HeliumTissuePressures[compartment] +
                                                                        ((diveModel.DiveProfile.HeliumPressureAtDepth -
                                                                        diveModel.DiveProfile.HeliumTissuePressures[compartment]) *
                                                                        (1.0f - Math.Pow(2.0f, -(diveStep.Time / diveModel.HeliumHalfTimes[compartment])))), 4);
        }

        private void CalculateTotalTissuesPressure(int compartment)
        {
            diveModel.DiveProfile.TotalTissuePressures[compartment] = diveModel.DiveProfile.HeliumTissuePressures[compartment] +
                                                                       diveModel.DiveProfile.NitrogenTissuePressures[compartment];
        }
    }
}