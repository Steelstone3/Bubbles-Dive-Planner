using System;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;

namespace BubblesDivePlanner.Commands
{
    public class TissuePressure : IDiveStageCommand
    {
        private readonly IDiveModel diveModel;
        private readonly IDiveStep diveStep;

        public TissuePressure(IDiveModel diveModel, IDiveStep diveStep)
        {
            this.diveModel = diveModel;
            this.diveStep = diveStep;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < diveModel.Compartments; compartment++)
            {
                CalculateNitrogenTissuePressures(compartment);
                CalculateHeliumTissuePressures(compartment);
                CalculateTotalTissuePressures(compartment);
            }
        }

        private void CalculateNitrogenTissuePressures(int compartment)
        {
            diveModel.DiveProfile.NitrogenTissuePressures[compartment] = (float)Math.Round(diveModel.DiveProfile.NitrogenTissuePressures[compartment] +
                                                                        ((diveModel.DiveProfile.NitrogenAtPressure -
                                                                        diveModel.DiveProfile.NitrogenTissuePressures[compartment]) *
                                                                        (1.0f - Math.Pow(2.0f, -(diveStep.Time / diveModel.NitrogenHalfTimes[compartment])))), 4);
        }

        private void CalculateHeliumTissuePressures(int compartment)
        {
            diveModel.DiveProfile.HeliumTissuePressures[compartment] = (float)Math.Round(diveModel.DiveProfile.HeliumTissuePressures[compartment] +
                                                                        ((diveModel.DiveProfile.HeliumAtPressure -
                                                                        diveModel.DiveProfile.HeliumTissuePressures[compartment]) *
                                                                        (1.0f - Math.Pow(2.0f, -(diveStep.Time / diveModel.HeliumHalfTimes[compartment])))), 4);
        }

        private void CalculateTotalTissuePressures(int compartment)
        {
            diveModel.DiveProfile.TotalTissuePressures[compartment] = diveModel.DiveProfile.HeliumTissuePressures[compartment] +
                                                                       diveModel.DiveProfile.NitrogenTissuePressures[compartment];
        }
    }
}