using System;
using System.Collections.Generic;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveResult;
using Xunit;

namespace DivePlannerTests
{
    public class CurrentDiveStepUserInterfaceShould
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

        [Fact(Skip="Needs Implementing")]
        public void PopulateCurrentDiveStepInResults()
        {

        }

        [Fact(Skip = "Tests Needs implementing")]
        public void RaisePropertyChangeWhenCurrentDiveStepInResultsIsPopulated()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}