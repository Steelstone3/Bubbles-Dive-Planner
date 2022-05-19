using System.Collections.Generic;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class PublishResultsCommandShould
    {
        private IDiveModel _diveModel = new Zhl16BuhlmannModel();
        private IResultModel _resultViewModel = new ResultViewModel();

        [Fact]
        public void PopulateDiveResultsModelOutputStage()
        {
            //Arrange
            var diveProfileModelStub = SetupDiveProfileModelStub();
            _diveModel.DiveProfile = diveProfileModelStub.Object;
            var diveStepModelStub = SetupDiveStepModelStub();

            var diveStage = new PublishResultsCommand(_diveModel, diveStepModelStub.Object, _resultViewModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(diveStepModelStub.Object, _resultViewModel.DiveStepModel);
            Assert.Equal(diveProfileModelStub.Object, _resultViewModel.DiveProfileModel);
        }

        private Mock<IDiveStepModel> SetupDiveStepModelStub()
        {
            Mock<IDiveStepModel> diveStepModelStub = new();
            diveStepModelStub.Setup(x => x.Depth).Returns(50);
            diveStepModelStub.Setup(x => x.Time).Returns(10);
            diveStepModelStub.Setup(x => x.DeepClone()).Returns(diveStepModelStub.Object);

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
            diveProfileModelStub.Setup(x => x.DeepClone()).Returns(diveProfileModelStub.Object);
            return diveProfileModelStub;
        }
    }
}