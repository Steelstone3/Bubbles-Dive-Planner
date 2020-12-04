using Xunit;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.Controllers.DiveInformationControllers;
using DivePlannerMk3.ViewModels.DivePlan;

namespace DivePlannerTests
{
    public class DiveBoundariesUiTests
    {
        private DiveBoundariesViewModel _diveBoundaries = new DiveBoundariesViewModel();

        [Fact]
        public void DiveBoundariesModelCanBeSetTest()
        {
            //Act
            _diveBoundaries.DiveCeiling = 10;

            //Assert
            Assert.Equal(10, _diveBoundaries.DiveCeiling);
        }
    }
}