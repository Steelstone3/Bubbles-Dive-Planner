using BubblesDivePlanner.Controllers.Cylinders;
using BubblesDivePlanner.Controllers.Interfaces;
using BubblesDivePlanner.ViewModels.Cylinders;
using BubblesDivePlanner.ViewModels.Models;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Cylinders
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