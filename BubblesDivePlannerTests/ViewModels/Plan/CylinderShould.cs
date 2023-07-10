using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public class CylinderShould
    {
        private readonly ICylinder cylinder = new Cylinder();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.NotNull(cylinder.Name);
            Assert.Equal(0u, cylinder.InitialPressurisedVolume);
            Assert.Equal(0u, cylinder.Volume);
            Assert.Equal(0u, cylinder.Pressure);
            Assert.NotNull(cylinder.GasMixture);
            Assert.NotNull(cylinder.GasUsage);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Cylinder cylinderVM = (Cylinder)cylinder;
            Mock<IGasMixture> gasMixture = new();
            Mock<IGasUsage> gasUsage = new();
            List<string> viewModelEvents = new();
            cylinderVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            cylinderVM.Name = "EAN32";
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

        [Fact]
        public void RaisePropertyChangedVolumeAndPressure()
        {
            //Arrange
            Cylinder cylinderVM = (Cylinder)cylinder;
            List<string> viewModelEvents = new();
            cylinderVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            cylinderVM.Volume = 12;
            cylinderVM.Pressure = 200;

            //Assert
            Assert.Contains(nameof(cylinderVM.Volume), viewModelEvents);
            Assert.Contains(nameof(cylinderVM.Pressure), viewModelEvents);
            Assert.Contains(nameof(cylinderVM.InitialPressurisedVolume), viewModelEvents);
            // Assert.Contains(nameof(cylinderVM.GasUsage.GasRemaining), viewModelEvents);
        }
    }
}