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

        [Fact]
        public void RaisePropertyChangedOxygen()
        {
            //Arrange
            GasMixture gasMixtureVM = (GasMixture)gasMixture;
            List<string> viewModelEvents = new();
            gasMixtureVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            gasMixtureVM.Oxygen = 21f;

            //Assert
            Assert.Contains(nameof(gasMixtureVM.Oxygen), viewModelEvents);
            Assert.Contains(nameof(gasMixtureVM.Nitrogen), viewModelEvents);
            Assert.Contains(nameof(gasMixtureVM.MaximumOperatingDepth), viewModelEvents);
        }

        [Fact]
        public void RaisePropertyChangedHelium()
        {
            //Arrange
            GasMixture gasMixtureVM = (GasMixture)gasMixture;
            List<string> viewModelEvents = new();
            gasMixtureVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            gasMixtureVM.Helium = 10f;

            //Assert
            Assert.Contains(nameof(gasMixtureVM.Helium), viewModelEvents);
            Assert.Contains(nameof(gasMixtureVM.Nitrogen), viewModelEvents);
        }
    }
}