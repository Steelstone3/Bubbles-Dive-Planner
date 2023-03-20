using BubblesDivePlanner.Cylinders.CylinderSetup;
using Xunit;

namespace BubblesDivePlannerTests.Cylinders.CylinderSetup
{
    public class CylinderPrototypeShould
    {
        [Fact]
        public void CreateCloneCylinder()
        {
            //Arrange 
            ICylinderSetupModel cylinderSetup = new CylinderSetupViewModel();
            ICylinderPrototype cylinderPrototype = new CylinderPrototype();

            //Act
            var clonedCylinderSetup = cylinderPrototype.DeepClone(cylinderSetup);

            //Assert
            Assert.NotSame(cylinderSetup, clonedCylinderSetup);
        }
    }
}