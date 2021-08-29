using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class DiveStepViewModelShould
    {
        private DiveStepViewModel _diveStep = new();
        
        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            //Arrange
           
            int depth = 50;
            int time = 10;
            
            var viewModelEvents = new List<string>();
            _diveStep.PropertyChanged += ((sender, e) => viewModelEvents.Add(e.PropertyName));

            //Act
            _diveStep.Depth = depth;
            _diveStep.Time = time;

            //Assert
            Assert.Contains(nameof(_diveStep.Depth), viewModelEvents);
            Assert.Contains(nameof(_diveStep.Time), viewModelEvents);
            
            Assert.Equal(depth, _diveStep.Depth);
            Assert.Equal(time, _diveStep.Time);
        }
        
        [Theory]
        [InlineData(50,10,55,true)]
        [InlineData(60,10,55,false)]
        [InlineData(-1,10,55,false)]
        [InlineData(101,10,120,false)]
        [InlineData(50,0,55,false)]
        [InlineData(50,101,55,false)]
        public void ValidateDiveStepParameters(int depth, int time, double maximumOperatingDepth, bool expectedResult)
        {
            var result = _diveStep.ValidateDiveStep(depth, time, maximumOperatingDepth);

            Assert.Equal(expectedResult, result);
        }
    }
}