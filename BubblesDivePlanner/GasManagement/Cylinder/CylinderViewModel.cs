using BubblesDivePlanner.GasManagement.GasMixture;
using BubblesDivePlanner.GasManagement.GasUsage;
using ReactiveUI;

namespace BubblesDivePlanner.GasManagement.Cylinder
{
    public class CylinderViewModel : ReactiveObject, ICylinderModel
    {
        public CylinderViewModel(IGasMixtureController gasMixtureController)
        {
            GasMixture = new GasMixtureViewModel(gasMixtureController);
        }

        private int _cylinderVolume;
        public int CylinderVolume
        {
            get => _cylinderVolume;
            set => this.RaiseAndSetIfChanged(ref _cylinderVolume, value);
        }

        private int _cylinderPressure;
        public int CylinderPressure
        {
            get => _cylinderPressure;
            set => this.RaiseAndSetIfChanged(ref _cylinderPressure, value);
        }

        private IGasMixtureModel _gasMixture;
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
    }
}