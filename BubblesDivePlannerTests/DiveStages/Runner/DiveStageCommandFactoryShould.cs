using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages.Runner
{
    public class DiveStageCommandFactoryShould
    {
        [Fact]
        public void CreateDiveStages() 
        {
            //Arrange
            var diveModelDummy = new Mock<IDiveModel>();
            var diveStepModelDummy = new Mock<IDiveStepModel>();
            var selectedCylinderModelDummy = new Mock<ICylinderSetupModel>();
            var resultModelDummy = new Mock<IResultModel>();
            IDiveStageCommandFactory diveStageCommandFactory = new DiveStageCommandFactory(diveModelDummy.Object, diveStepModelDummy.Object, selectedCylinderModelDummy.Object, resultModelDummy.Object);

            //Assert
            var actualDiveStages = diveStageCommandFactory.CreateDiveStages();

            //Act
            Assert.NotEmpty(actualDiveStages);
        }
    }
}