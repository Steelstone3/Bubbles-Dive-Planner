using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using Xunit;

namespace name
{
    public class GasMixtureShould
    {
        private readonly IGasMixture gasMixture = new GasMixture();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.Equal(0f, gasMixture.Oxygen);
            Assert.Equal(0f, gasMixture.Helium);
            Assert.Equal(100f, gasMixture.Nitrogen);
            Assert.Equal(0f, gasMixture.MaximumOperatingDepth);
        }

        // [Fact]
        // public void RaisePropertyChanged()
        // {
        //     //Arrange
        //     Cylinder cylinderVM = (Cylinder)cylinder;
        //     Mock<IGasMixture> gasMixture = new();
        //     Mock<IGasUsage> gasUsage = new();
        //     List<string> viewModelEvents = new();
        //     cylinderVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

        //     //Act
        //     cylinderVM.Name = "EAN32";
        //     cylinderVM.InitialPressurisedVolume = 2400;
        //     cylinderVM.Volume = 12;
        //     cylinderVM.Pressure = 200;
        //     cylinderVM.GasMixture = gasMixture.Object;
        //     cylinderVM.GasUsage = gasUsage.Object;

        //     //Assert
        //     Assert.Contains(nameof(cylinderVM.Name), viewModelEvents);
        //     Assert.Contains(nameof(cylinderVM.InitialPressurisedVolume), viewModelEvents);
        //     Assert.Contains(nameof(cylinderVM.Volume), viewModelEvents);
        //     Assert.Contains(nameof(cylinderVM.Pressure), viewModelEvents);
        //     Assert.Contains(nameof(cylinderVM.GasMixture), viewModelEvents);
        //     Assert.Contains(nameof(cylinderVM.GasUsage), viewModelEvents);
        // }
    }
}