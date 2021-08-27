using System;
using System.Collections.Generic;
using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.Results;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.DiveApplication.DiveResults
{
    public class DiveResultsViewModelShould
    {
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveResultsStepOutputModel _resultsStepOutput = new DiveResultsStepOutputModel();

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
        public void PopulateDiveResultsModelOutputStage()
        {
            //Arrange
            var diveStage = new DiveStageResults(_diveModel.CompartmentCount, _resultsStepOutput, _diveProfile);

            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();
                
                //Assert
                Assert.Equal(Math.Round(_diveProfile.CompartmentLoad[i], 2), _resultsStepOutput.DiveProfileStepOutput[i].CompartmentLoadResult);
                Assert.Equal(Math.Round(_diveProfile.TissuePressuresTotal[i], 2), _resultsStepOutput.DiveProfileStepOutput[i].TissuePressureResult);
                Assert.Equal(Math.Round(_diveProfile.MaxSurfacePressures[i], 2), _resultsStepOutput.DiveProfileStepOutput[i].MaximumSurfacePressureResult);
                Assert.Equal(Math.Round(_diveProfile.ToleratedAmbientPressures[i], 2), _resultsStepOutput.DiveProfileStepOutput[i].ToleratedAmbientPressureResult);
            }
        }

        [Fact(Skip="Test needs implementing")]
        public void ConvertDiveResultsModelToViewModelOutputStage()
        {

        }

        [Fact(Skip = "Tests Needs implementing")]
        public void RaisePropertyChangeWhenDiveResultsModelIsPopulated()
        {
            //Arrange
            
            //Act

            //Assert
        }
    }
}