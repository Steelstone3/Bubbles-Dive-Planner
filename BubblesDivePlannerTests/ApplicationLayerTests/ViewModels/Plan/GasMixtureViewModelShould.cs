using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class GasMixtureViewModelShould
    {
        private readonly GasMixtureViewModel _gasMixtureViewModel = new();
        
        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            //Arrange
            string gasName = "Air";
            int oxygen = 21;
            int helium = 1;
            int nitrogen = 100 - oxygen - helium;
            
            var viewModelEvents = new List<string>();
            _gasMixtureViewModel.PropertyChanged += ((sender, e) => viewModelEvents.Add(e.PropertyName));

            //Act
            _gasMixtureViewModel.GasName = gasName;
            _gasMixtureViewModel.Oxygen = oxygen;
            _gasMixtureViewModel.Helium = helium;

            //Assert
            Assert.Contains(nameof(_gasMixtureViewModel.GasName), viewModelEvents);
            Assert.Contains(nameof(_gasMixtureViewModel.Oxygen), viewModelEvents);
            Assert.Contains(nameof(_gasMixtureViewModel.Helium), viewModelEvents);
            
            Assert.Equal(gasName, _gasMixtureViewModel.GasName);
            Assert.Equal(oxygen, _gasMixtureViewModel.Oxygen);
            Assert.Equal(helium, _gasMixtureViewModel.Helium);
            Assert.Equal(nitrogen, _gasMixtureViewModel.Nitrogen);
        }
    }
}