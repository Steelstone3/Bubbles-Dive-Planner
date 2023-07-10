using System;
using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public class GasUsageShould
    {
        private readonly IGasUsage gasMixture = new GasUsage();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.Equal(0u, gasMixture.GasRemaining);
            Assert.Equal(0u, gasMixture.GasUsed);
            Assert.Equal(0u, gasMixture.SurfaceAirConsumptionRate);
        }

        // [Fact]
        // public void RaisePropertyChangedOxygen()
        // {
        //     //Arrange
        //     GasMixture gasMixtureVM = (GasMixture)gasMixture;
        //     List<string> viewModelEvents = new();
        //     gasMixtureVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

        //     //Act
        //     gasMixtureVM.Oxygen = 21f;

        //     //Assert
        //     Assert.Contains(nameof(gasMixtureVM.Oxygen), viewModelEvents);
        //     Assert.Contains(nameof(gasMixtureVM.Nitrogen), viewModelEvents);
        //     Assert.Contains(nameof(gasMixtureVM.MaximumOperatingDepth), viewModelEvents);
        // }
    }
}