using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlanner.DiveStep;
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
            IDiveStageCommandFactory diveStageCommandFactory = new DiveStageCommandFactory(diveModelDummy.Object, diveStepModelDummy.Object, selectedCylinderModelDummy.Object);

            //Assert
            var actualDiveStages = diveStageCommandFactory.CreateDiveStages();

            //Act
            Assert.NotEmpty(actualDiveStages);
        }
    }
}