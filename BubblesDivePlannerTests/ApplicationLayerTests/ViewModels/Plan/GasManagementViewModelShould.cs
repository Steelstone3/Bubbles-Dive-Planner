using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class GasManagementViewModelShould
    {
        private readonly GasManagementViewModel _gasManagement = new();

        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            //Arrange
            var cylinderVolume = 12;
            var cylinderPressure = 200;
            var sacRate = 12;
            
            var initialCylinderTotalVolume = 2400;
            var gasUsedForStep = 1500;
            var gasRemaining = 900;
            
            var viewModelEvents = new List<string>();
            _gasManagement.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);
            
            //Act
            _gasManagement.CylinderVolume = cylinderVolume;
            _gasManagement.CylinderPressure = cylinderPressure;
            _gasManagement.SacRate = sacRate;
            _gasManagement.GasUsedForStep = gasUsedForStep;
            
            //Assert
            Assert.Equal(cylinderVolume, _gasManagement.CylinderVolume);
            Assert.Equal(cylinderPressure, _gasManagement.CylinderPressure);
            Assert.Equal(sacRate, _gasManagement.SacRate);
            Assert.Equal(initialCylinderTotalVolume, _gasManagement.InitialCylinderTotalVolume);
            Assert.Equal(gasUsedForStep, _gasManagement.GasUsedForStep);
            Assert.Equal(gasRemaining, _gasManagement.GasRemaining);

            Assert.Equal(nameof(_gasManagement.CylinderVolume), viewModelEvents[0]);
            Assert.Equal(nameof(_gasManagement.CylinderPressure), viewModelEvents[1]);
            Assert.Equal(nameof(_gasManagement.InitialCylinderTotalVolume), viewModelEvents[2]);
            Assert.Equal(nameof(_gasManagement.GasRemaining), viewModelEvents[3]);
            Assert.Equal(nameof(_gasManagement.SacRate), viewModelEvents[4]);
            Assert.Equal(nameof(_gasManagement.GasUsedForStep), viewModelEvents[5]);
        }
        
        [Theory]
        [InlineData(200, 12, 12, true)]
        [InlineData(301, 12, 12, false)]
        [InlineData(49, 12, 12, false)]
        [InlineData(200, 31, 12, false)]
        [InlineData(200, 2, 12, false)]
        [InlineData(200, 12, 31, false)]
        [InlineData(200, 12, 4, false)]
        public void ValidateGasManagementSetupParameters(int cylinderPressure, int cylinderVolume, int sacRate,
            bool expectedResult)
        {
            var result = _gasManagement.ValidateGasManagement(cylinderVolume, cylinderPressure, sacRate);

            Assert.Equal(expectedResult, result);
        }
        
        [Fact]
        public void HaveADefaultVisibiltyState()
        {
            //Assert
            Assert.False(_gasManagement.IsGasUsageVisible);
            Assert.True(_gasManagement.IsUiVisible);
            Assert.True(_gasManagement.IsUiEnabled);
        }
        
        //TODO AH this test needs implementing
        [Fact(Skip = "Test needs Implementing")]
        public void HaveAPostGasManagementVisibiltyState()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_gasManagement.IsGasUsageVisible);
            Assert.False(_gasManagement.IsUiVisible);
            Assert.False(_gasManagement.IsUiEnabled);
        }
    }
}