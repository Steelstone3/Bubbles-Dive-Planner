using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class GasMixtureSelectorViewModelShould
    {
        private Mock<IGasMixtureModel> _gasMixtureModel = new();
        private Mock<IGasMixtureViewModel> _gasMixtureViewModel = new();
        
        private readonly GasMixtureSelectorViewModel _gasMixtureSelectorViewModel = new();
        
        [Fact]
        public void HaveAtLeastOneGasMixture()
        {
            //Assert
            Assert.NotNull(_gasMixtureSelectorViewModel.GasMixtures);
            Assert.NotEmpty(_gasMixtureSelectorViewModel.GasMixtures);
        }

        [Theory]
        [InlineData(21, 79, 0, "Air")]
        [InlineData(10, 70, 20, "Heliox")]
        [InlineData(32, 68, 0, "EAN32")]
        public void AllowGasMixturesToBeAdded(double oxygen, double nitrogen, double helium, string gasName)
        {
            //Arrange
            _gasMixtureModel = SetupGasMixtureModel(oxygen, nitrogen, helium, gasName);

            //Act
            _gasMixtureSelectorViewModel.GasMixtures.Add(_gasMixtureModel.Object);

            //Assert
            Assert.Equal(2, _gasMixtureSelectorViewModel.GasMixtures.Count);
        }

        [Fact]
        public void AllowGasSelectorModelToBeSet()
        {
            //Arrange
            var maximumOperatingDepth = 10;

            //Act
            _gasMixtureSelectorViewModel.SelectedGasMixture = _gasMixtureModel.Object;
            _gasMixtureSelectorViewModel.MaximumOperatingDepth = 10;
            _gasMixtureSelectorViewModel.NewGasMixture = _gasMixtureViewModel.Object;


            //Assert
            Assert.Equal(_gasMixtureModel.Object, _gasMixtureSelectorViewModel.SelectedGasMixture);
            Assert.Equal(maximumOperatingDepth, _gasMixtureSelectorViewModel.MaximumOperatingDepth);
            Assert.Equal(_gasMixtureViewModel.Object, _gasMixtureSelectorViewModel.NewGasMixture);
        }

        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            //Arrange
            var viewModelEvents = new List<string>();

            _gasMixtureSelectorViewModel.PropertyChanged += ((sender, e) => viewModelEvents.Add(e.PropertyName));

            //Act
            _gasMixtureSelectorViewModel.SelectedGasMixture = _gasMixtureModel.Object;

            //Assert
            Assert.Contains(nameof(_gasMixtureSelectorViewModel.MaximumOperatingDepth), viewModelEvents);
            Assert.Contains(nameof(_gasMixtureSelectorViewModel.SelectedGasMixture), viewModelEvents);
        }

        [Theory]
        [InlineData(100, -1, "Negative Helium")]
        [InlineData(-1, 100, "Negative Oxygen")]
        public async void NotAllowDiveStepExecutionIfGasMixtureHasNegativeValues(double oxygen, double helium,
            string gasName)
        {
            //Arrange
            _gasMixtureViewModel = SetupGasMixtureViewModel(oxygen, helium, gasName);

            //Act
            _gasMixtureSelectorViewModel.NewGasMixture = _gasMixtureViewModel.Object;

            var canExecute = await _gasMixtureSelectorViewModel.CanAddGasMixture.FirstAsync();

            //Assert
            Assert.False(canExecute);
        }

        [Theory]
        [InlineData(25, 80, "Loads of Helium")]
        [InlineData(80, 25, "Loads of Oxygen")]
        [InlineData(0, 101, "Helium")]
        [InlineData(101, 0, "O2")]
        public async void NotAllowDiveStepExecutionIfGasMixtureIsOver100Percent(double oxygen, double helium,
            string gasName)
        {
            //Arrange
            _gasMixtureViewModel = SetupGasMixtureViewModel(oxygen, helium, gasName);

            //Act
            _gasMixtureSelectorViewModel.NewGasMixture = _gasMixtureViewModel.Object;

            var canExecute = await _gasMixtureSelectorViewModel.CanAddGasMixture.FirstAsync();

            //Assert
            Assert.False(canExecute);
        }

        [Theory]
        [InlineData(1, 0, "Oxygen Starved")]
        [InlineData(4, 0, "Oxygen Starved 2")]
        public async void NotAllowDiveStepExecutionIfGasMixtureOxygenLevelsAreTooLow(double oxygen, double helium,
            string gasName)
        {
            //Arrange
            _gasMixtureViewModel = SetupGasMixtureViewModel(oxygen, helium, gasName);

            //Act
            _gasMixtureSelectorViewModel.NewGasMixture = _gasMixtureViewModel.Object;

            var canExecute = await _gasMixtureSelectorViewModel.CanAddGasMixture.FirstAsync();

            //Assert
            Assert.False(canExecute);
        }

        [Fact]
        public void ValidateInValidGasManagementParameters()
        {
            var invalidResult = _gasMixtureSelectorViewModel.ValidateGasMixture(null);
            Assert.False(invalidResult);
        }

        [Fact]
        public void ValidateValidGasManagementParameters()
        {
            var validResult =
                _gasMixtureSelectorViewModel.ValidateGasMixture(_gasMixtureSelectorViewModel.GasMixtures.First());
            Assert.True(validResult);
        }
        
        private Mock<IGasMixtureModel> SetupGasMixtureModel(double oxygen, double nitrogen, double helium,
            string gasName)
        {
            _gasMixtureModel.Setup(x => x.Oxygen).Returns(oxygen);
            _gasMixtureModel.Setup(x => x.Nitrogen).Returns(nitrogen);
            _gasMixtureModel.Setup(x => x.Helium).Returns(helium);
            _gasMixtureModel.Setup(x => x.GasName).Returns(gasName);
            return _gasMixtureModel;
        }
        
        private Mock<IGasMixtureViewModel> SetupGasMixtureViewModel(double oxygen, double helium, string gasName)
        {
            _gasMixtureViewModel.Setup(x => x.Oxygen).Returns(oxygen);
            _gasMixtureViewModel.Setup(x => x.Helium).Returns(helium);
            _gasMixtureViewModel.Setup(x => x.Nitrogen).Returns(100 - oxygen - helium);
            _gasMixtureViewModel.Setup(x => x.GasName).Returns(gasName);
            return _gasMixtureViewModel;
        }
    }
}