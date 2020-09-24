using System.Collections.Generic;
using DivePlannerMk3.Controllers.DiveStages;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageResults : DiveStage
    {
        private int _compartmentCount;
        private DiveProfileResultsListViewModel _results;
        private IDiveProfile _diveProfile;

        public DiveStageResults(int compartmentCount, DiveProfileResultsListViewModel results, IDiveProfile diveProfile)
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
            stepResult.TissuePressureResult = _diveProfile.TissuePressuresTotal[Compartment];
            stepResult.CompartmentLoadResult = _diveProfile.CompartmentLoad[Compartment];
            stepResult.MaximumSurfacePressureResult = _diveProfile.MaxSurfacePressures[Compartment];
            stepResult.ToleratedAmbientPressureResult = _diveProfile.ToleratedAmbientPressures[Compartment];
            
            _results.DiveProfileStepOutput.Add(stepResult);
        }
    }
}