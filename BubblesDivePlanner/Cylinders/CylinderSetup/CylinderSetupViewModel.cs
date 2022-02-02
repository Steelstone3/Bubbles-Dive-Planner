using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSetup
{
    public class CylinderSetupViewModel : ReactiveObject, ICylinderSetupModel
    {
        private string _cylinderName = "Air";
        public string CylinderName 
        {
            get=> _cylinderName;
            set=> this.RaiseAndSetIfChanged(ref _cylinderName, value);
        }
        
        private int _cylinderVolume;
        public int CylinderVolume
        {
            get => _cylinderVolume;
            set
            {
                this.RaiseAndSetIfChanged(ref _cylinderVolume, value);
                GasUsage.InitialPressurisedCylinderVolume = new GasUsageController().CalculateInitialPressurisedCylinderVolume(CylinderVolume, CylinderPressure);
            }
        }

        private int _cylinderPressure;
        public int CylinderPressure
        {
            get => _cylinderPressure;
            set
            {
                this.RaiseAndSetIfChanged(ref _cylinderPressure, value);
                GasUsage.InitialPressurisedCylinderVolume = new GasUsageController().CalculateInitialPressurisedCylinderVolume(CylinderVolume, CylinderPressure);
            }
        }

        private IGasMixtureModel _gasMixture = new GasMixtureViewModel();
        public IGasMixtureModel GasMixture
        {
            get => _gasMixture;
            set => this.RaiseAndSetIfChanged(ref _gasMixture, value);
        }

        private IGasUsageModel _gasUsage = new GasUsageViewModel();
        public IGasUsageModel GasUsage
        {
            get => _gasUsage;
            set => this.RaiseAndSetIfChanged(ref _gasUsage, value);
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
    }
}