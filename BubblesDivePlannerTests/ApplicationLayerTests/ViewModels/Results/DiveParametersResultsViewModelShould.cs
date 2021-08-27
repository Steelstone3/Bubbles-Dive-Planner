using BubblesDivePlanner.ViewModels.Result;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Results
{
    public class DiveParametersResultsViewModelShould
    {
        private DiveParametersResultViewModel _diveParametersResultViewModel = new DiveParametersResultViewModel();

        [Fact]
        public void RaisePropertyChangedWhenDepthPropertyIsSet()
        {
            //Arrange
            string depthEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => depthEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.Depth = 50;

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.Depth), depthEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenTimePropertyIsSet()
        {
            //Arrange
            string timeEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => timeEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.Time = 50;

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.Time), timeEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenGasNamePropertyIsSet()
        {
            //Arrange
            string gasNameEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => gasNameEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.GasName = "Air";

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.GasName), gasNameEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenDiveCeilingPropertyIsSet()
        {
            //Arrange
            string diveCeilingEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => diveCeilingEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.DiveCeiling = 15;

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.DiveCeiling), diveCeilingEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenDiveModelUsedPropertyIsSet()
        {
            //Arrange
            string diveModelUsedEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => diveModelUsedEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.DiveModelUsed = "ZHL16";

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.DiveModelUsed), diveModelUsedEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenDiveProfileStepHeaderPropertyIsSet()
        {
            //Arrange
            string diveProfileStepHeaderEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => diveProfileStepHeaderEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.DiveProfileStepHeader = "Dive Header";

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.DiveProfileStepHeader), diveProfileStepHeaderEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenHeliumPropertyIsSet()
        {
            //Arrange
            string heliumEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => heliumEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.Helium = 1;

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.Helium), heliumEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenOxygenPropertyIsSet()
        {
            //Arrange
            string oxygenEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => oxygenEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.Oxygen = 21;

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.Oxygen), oxygenEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenNitrogenPropertyIsSet()
        {
            //Arrange
            string nitrogenEvent = "Not Fired";
            _diveParametersResultViewModel.PropertyChanged += ((sender, e) => nitrogenEvent = e.PropertyName);

            //Act
            _diveParametersResultViewModel.Nitrogen = 78;

            //Assert
            Assert.Equal(nameof(_diveParametersResultViewModel.Nitrogen), nitrogenEvent);
        }
    }
}