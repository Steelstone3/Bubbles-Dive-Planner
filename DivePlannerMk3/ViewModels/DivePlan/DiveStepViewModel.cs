using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers.DiveInformationControllers;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class DiveStepViewModel : ViewModelBase, IDiveStepModel
    {
        public DiveStepViewModel()
        {
            //subscribe to event

        }

        private int _depth;
        public int Depth 
        { 
            get => _depth;
            set => this.RaiseAndSetIfChanged(ref _depth, value);
        }

        private int _time;
        public int Time
        { 
            get => _time;
            set => this.RaiseAndSetIfChanged(ref _time, value);
        }

        private double _maximumOperatingDepth;
        public double MaximumOperatingDepth
        {
            get => _maximumOperatingDepth;
            set => this.RaiseAndSetIfChanged(ref _maximumOperatingDepth, value);
        }

        private void UpdateMaximumOperatingDepth(double oxygen)
        {
            var controller = new DiveBounderiesController();

            //TODO AH populate parameter from event arguments
            MaximumOperatingDepth = controller.CalculateMaximumOperatingDepth(oxygen);
        }
    }
}