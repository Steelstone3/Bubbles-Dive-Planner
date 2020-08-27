using System;
using System.Collections.Generic;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveResult;
using Xunit;

namespace DivePlannerTests
{
    public class DiveResultsUiTests
    {
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveResultsModel _results = new DiveResultsModel();

        //TODO AH Test to check raise property changed on the results view model

        private DiveProfile _diveProfile = new DiveProfile()
        {
            TissuePressuresTotal = new List<double>()
            {
                1.101100,1.111111,1.111111,1.111111,
                1.1111,1.1111,1.1111,1.1111,
                1.11111,1.11111,1.11111,1.11111,
                1.1111,1.1111,1.1111,1.1111,
            },

            CompartmentLoad = new List<double>()
            {
                2.222222, 2.222222, 2.222222, 2.222222,
                2.2222, 2.2222, 2.2222, 2.2222,
                2.22222, 2.22222, 2.22222, 2.22222,
                2.2222, 2.2222, 2.2222, 2.2222,
            },

            MaxSurfacePressures = new List<double>()
            {
                3.333333, 3.333333, 3.333333, 3.333333,
                3.3333, 3.3333, 3.3333, 3.3333,
                3.33333, 3.33333, 3.33333, 3.33333,
                3.3333, 3.3333, 3.3333, 3.3333,
            },

            ToleratedAmbientPressures = new List<double>()
            {
                4.444444,4.444444,4.444444,4.444444,
                4.4444,4.4444,4.4444,4.4444,
                4.44444,4.44444,4.44444,4.44444,
                4.4444,4.4444,4.4444,4.4444,
            },
        };

        [Fact]
        private void PreDiveStageStepInfoOutputTwoDecimalPlacesTest()
        {
            //Arrange
            var diveStage = new DiveStageResults(_diveModel.CompartmentCount, _results, _diveProfile);

            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();
                
                //Assert
                Assert.Equal(Math.Round(_diveProfile.CompartmentLoad[i], 2), _results.DiveProfileStepOutput[i].CompartmentLoadResult);
                Assert.Equal(Math.Round(_diveProfile.TissuePressuresTotal[i], 2), _results.DiveProfileStepOutput[i].TissuePressureResult);
                Assert.Equal(Math.Round(_diveProfile.MaxSurfacePressures[i], 2), _results.DiveProfileStepOutput[i].MaximumSurfacePressureResult);
                Assert.Equal(Math.Round(_diveProfile.ToleratedAmbientPressures[i], 2), _results.DiveProfileStepOutput[i].ToleratedAmbientPressureResult);
            }
        }
    }
}