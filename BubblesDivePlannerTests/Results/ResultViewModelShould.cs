using System.Collections.Generic;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Results
{
    public class ResultViewModelShould
    {
        private ResultViewModel _resultViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            var diveProfileModelStub = SetupDiveProfileModelStub();
            var diveStepModelStub = SetupDiveStepModelStub();

            //Act
           _resultViewModel.DiveProfileModel = diveProfileModelStub.Object;
           _resultViewModel.DiveStepModel = diveStepModelStub.Object;

            //Assert
            Assert.Equal(diveStepModelStub.Object, _resultViewModel.DiveStepModel);
            Assert.Equal(diveProfileModelStub.Object, _resultViewModel.DiveProfileModel);
        }

        private Mock<IDiveStepModel> SetupDiveStepModelStub()
        {
            Mock<IDiveStepModel> diveStepModelStub = new();
            diveStepModelStub.Setup(x => x.Depth).Returns(50);
            diveStepModelStub.Setup(x => x.Time).Returns(10);

            return diveStepModelStub;
        }

        private Mock<IDiveProfileModel> SetupDiveProfileModelStub()
        {
            var diveProfileStubList = new List<double>() { 2.222222, 2.222222, 2.222222, 2.222222, 2.2222, 2.2222, 2.2222, 2.2222, 2.22222, 2.22222, 2.22222, 2.22222, 2.2222, 2.2222, 2.2222, 2.2222 };

            Mock<IDiveProfileModel> diveProfileModelStub = new();
            diveProfileModelStub.Setup(x => x.PressureOxygen).Returns(4);
            diveProfileModelStub.Setup(x => x.PressureHelium).Returns(4);
            diveProfileModelStub.Setup(x => x.PressureNitrogen).Returns(4);
            diveProfileModelStub.Setup(x => x.ToleratedAmbientPressures).Returns(diveProfileStubList);
            diveProfileModelStub.Setup(x => x.AValues).Returns(diveProfileStubList);
            diveProfileModelStub.Setup(x => x.BValues).Returns(diveProfileStubList);
            diveProfileModelStub.Setup(x => x.MaxSurfacePressures).Returns(diveProfileStubList);
            diveProfileModelStub.Setup(x => x.TissuePressuresNitrogen).Returns(diveProfileStubList);
            diveProfileModelStub.Setup(x => x.TissuePressuresHelium).Returns(diveProfileStubList);
            diveProfileModelStub.Setup(x => x.TissuePressuresTotal).Returns(diveProfileStubList);
            diveProfileModelStub.Setup(x => x.CompartmentLoad).Returns(diveProfileStubList);

            return diveProfileModelStub;
        }
    }
}