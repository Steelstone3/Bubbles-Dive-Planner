using System;
using BubblesDivePlanner.Contracts.Models.DiveModels;
using BubblesDivePlanner.Models.Results;

namespace BubblesDivePlanner.Commands.DiveStages
{
    public class DiveStageResults : DiveStage
    {
        private int _compartmentCount;
        private DiveResultsModel _results;
        private IDiveProfile _diveProfile;

        public DiveStageResults(int compartmentCount, DiveResultsModel results, IDiveProfile diveProfile)
        {
            _compartmentCount = compartmentCount;
            _results = results;
            _diveProfile = diveProfile;
        }


        public override void RunStage()
        {
            PopulateResults();
            CompartmentCountCheck(_compartmentCount - 1);
        }

        private void PopulateResults()
        {
            var stepResult = new DiveProfileStepOutputModel();

            stepResult.DiveProfileStepHeader = "Dive Step";
            stepResult.Compartment = Compartment + 1;
            stepResult.TissuePressureResult = Math.Round(_diveProfile.TissuePressuresTotal[Compartment], 2);
            stepResult.CompartmentLoadResult = Math.Round(_diveProfile.CompartmentLoad[Compartment], 2);
            stepResult.MaximumSurfacePressureResult = Math.Round(_diveProfile.MaxSurfacePressures[Compartment], 2);
            stepResult.ToleratedAmbientPressureResult = Math.Round(_diveProfile.ToleratedAmbientPressures[Compartment], 2);

            _results.DiveProfileStepOutput.Add(stepResult);
        }
    }
}