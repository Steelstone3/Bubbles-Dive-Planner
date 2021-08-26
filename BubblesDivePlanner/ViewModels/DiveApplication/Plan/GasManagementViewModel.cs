using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Controllers.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Plan
{
    public class GasManagementViewModel : ViewModelBase, IGasManagementModel
    {
        #region Gas Setup
        private GasManagementController _gasManagementController;

        public GasManagementViewModel()
        {
            IsUiVisible = true;
            _gasManagementController = new GasManagementController();
        }

        private int _cylinderVolume;
        public int CylinderVolume
        {
            get => _cylinderVolume;
            set
            {
                this.RaiseAndSetIfChanged(ref _cylinderVolume, value);
                CalculateInitialCylinderVolume();
            }
        }

        private int _cylinderPressure;
        public int CylinderPressure
        {
            get => _cylinderPressure;
            set
            {
                this.RaiseAndSetIfChanged(ref _cylinderPressure,value);
                CalculateInitialCylinderVolume();
            }
        }

        private int _sacRate;
        public int SacRate
        {
            get => _sacRate;
            set => this.RaiseAndSetIfChanged(ref _sacRate, value);
        }

        private int _initialCylinderTotalVolume;
        public int InitialCylinderTotalVolume
        {
            get => _initialCylinderTotalVolume;
            set 
            {
                this.RaiseAndSetIfChanged(ref _initialCylinderTotalVolume, value);
                ResetRemainingCylinderVolume();
            }
        }

        #endregion

        #region Gas Usage

        private bool _isGasUsageVisible = false;
        public bool IsGasUsageVisible
        {
            get => _isGasUsageVisible;
            set => this.RaiseAndSetIfChanged(ref _isGasUsageVisible, value);
        }

        private int _gasUsedForStep;
        public int GasUsedForStep
        {
            get => _gasUsedForStep;
            set
            {
                this.RaiseAndSetIfChanged(ref _gasUsedForStep, value);
                CalculateGasRemaining();
            }
        }

        private int _gasRemaining;
        public int GasRemaining
        {
            get => _gasRemaining;
            set => this.RaiseAndSetIfChanged(ref _gasRemaining, value);
        }

        #endregion

        public bool ValidateGasManagement(int cylinderVolume, int cylinderPressure, int sacRate)
        {
            return sacRate <= 30 && sacRate >= 5 &&
            cylinderPressure <= 300 && cylinderPressure >= 50 &&
            cylinderVolume <= 30 && cylinderVolume >= 3;
        }

        private void CalculateInitialCylinderVolume()
        {
            InitialCylinderTotalVolume = _gasManagementController.CalculateInitialGasVolume(CylinderVolume, CylinderPressure);
        }

        private void CalculateGasRemaining()
        {
            GasRemaining = _gasManagementController.CalculateGasRemaining(GasRemaining, GasUsedForStep);
        }

        private void ResetRemainingCylinderVolume()
        {
            GasRemaining = _gasManagementController.CalculateInitialGasVolume(CylinderVolume, CylinderPressure);
        }
    }
}
