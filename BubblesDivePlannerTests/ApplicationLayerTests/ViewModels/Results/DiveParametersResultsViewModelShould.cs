using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.Result;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Results
{
    public class DiveParametersResultsViewModelShould
    {
        private DiveParametersResultViewModel _diveParametersResultViewModel = new();

        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => viewModelEvents.Add(e.PropertyName));

            //Act
            _diveParametersResultViewModel.DiveProfileStepHeader = "Dive Header";

            _diveParametersResultViewModel.DiveModelUsed = "ZHL16";
            
            _diveParametersResultViewModel.Depth = 50;
            _diveParametersResultViewModel.Time = 10;
            
            _diveParametersResultViewModel.GasName = "Air";
            _diveParametersResultViewModel.Oxygen = 21;
            _diveParametersResultViewModel.Helium = 10;
            _diveParametersResultViewModel.Nitrogen = 69;
            _diveParametersResultViewModel.DiveCeiling = 55;


            //Assert
            Assert.Contains(nameof(_diveParametersResultViewModel.DiveProfileStepHeader), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.DiveModelUsed), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.Depth), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.Time), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.GasName), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.Oxygen), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.Helium), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.Nitrogen), viewModelEvents);
            Assert.Contains(nameof(_diveParametersResultViewModel.DiveCeiling), viewModelEvents);
            
            Assert.Equal("Dive Header", _diveParametersResultViewModel.DiveProfileStepHeader);
            Assert.Equal("ZHL16", _diveParametersResultViewModel.DiveModelUsed);
            Assert.Equal(50, _diveParametersResultViewModel.Depth);
            Assert.Equal(10, _diveParametersResultViewModel.Time);
            Assert.Equal("Air", _diveParametersResultViewModel.GasName);
            Assert.Equal(21, _diveParametersResultViewModel.Oxygen);
            Assert.Equal(10, _diveParametersResultViewModel.Helium);
            Assert.Equal(69, _diveParametersResultViewModel.Nitrogen);
            Assert.Equal(55, _diveParametersResultViewModel.DiveCeiling);
        }
    }
}