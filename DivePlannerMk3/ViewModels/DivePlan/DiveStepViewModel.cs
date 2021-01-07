using DivePlannerMk3.Contracts;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class DiveStepViewModel : ViewModelBase, IDiveStepModel
    {
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
    }
}