using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveResult;
using Xunit;

namespace DivePlannerTests
{
    public class CurrentDiveResultsUiTests
    {
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveProfileResultsListViewModel _results = new DiveProfileResultsListViewModel()
        {

        };

        private DiveProfile _diveProfile = new DiveProfile();


        [Theory(Skip = "Test needs implementing")]
        [InlineData()]
        private void PreDiveStageStepInfoOutputTest()
        {

            var diveStage = new DiveStageResults(_diveModel.CompartmentCount, _results, _diveProfile);

            /* stepResult.DiveProfileStepHeader = "Dive Step";
               stepResult.Compartment = Compartment + 1;
               stepResult.TissuePressureResult = _diveProfile.TissuePressuresTotal[Compartment];
               stepResult.CompartmentLoadResult = _diveProfile.CompartmentLoad[Compartment];
               stepResult.MaximumSurfacePressureResult = _diveProfile.MaxSurfacePressures[Compartment];
               stepResult.ToleratedAmbientPressureResult = _diveProfile.ToleratedAmbientPressures[Compartment];

               _results.DiveProfileStepOutput.Add(stepResult);*/
        }

        [Fact(Skip = "Test needs implementing")]
        private void DiveStageInfoLimitedToTwoDecimalPoints()
        {

        }

    }


}