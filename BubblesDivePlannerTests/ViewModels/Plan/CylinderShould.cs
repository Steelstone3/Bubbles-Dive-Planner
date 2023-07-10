using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public class CylinderShould
    {
        private readonly ICylinder planner = new Cylinder();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.NotNull(planner.Name);
            Assert.Equal(0u, planner.InitialPressurisedVolume);
            Assert.Equal(0u, planner.Volume);
            Assert.Equal(0u, planner.Pressure);
            Assert.NotNull(planner.GasMixture);
            Assert.NotNull(planner.GasUsage);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Cylinder cylinderVM = (Cylinder)planner;
            Mock<IGasMixture> gasMixture = new();
            Mock<IGasUsage> gasUsage = new();
            List<string> viewModelEvents = new();
            cylinderVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            cylinderVM.Name = "EAN32";
            cylinderVM.InitialPressurisedVolume = 2400;
            cylinderVM.Volume = 12;
            cylinderVM.Pressure = 200;
            cylinderVM.GasMixture = gasMixture.Object;
            cylinderVM.GasUsage = gasUsage.Object;

            //Assert
            Assert.Contains(nameof(cylinderVM.Name), viewModelEvents);
            Assert.Contains(nameof(cylinderVM.InitialPressurisedVolume), viewModelEvents);
            Assert.Contains(nameof(cylinderVM.Volume), viewModelEvents);
            Assert.Contains(nameof(cylinderVM.Pressure), viewModelEvents);
            Assert.Contains(nameof(cylinderVM.GasMixture), viewModelEvents);
            Assert.Contains(nameof(cylinderVM.GasUsage), viewModelEvents);
        }
    }
}