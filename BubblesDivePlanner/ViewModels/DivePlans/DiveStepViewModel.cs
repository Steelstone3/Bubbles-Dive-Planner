using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlans
{
    public class DiveStepViewModel : ReactiveObject, IDiveStepModel
    {
        private byte _depth;
        public byte Depth
        {
            get => _depth;
            set => this.RaiseAndSetIfChanged(ref _depth, value);
        }

        private byte _time;
        public byte Time
        {
            get => _time;
            set => this.RaiseAndSetIfChanged(ref _time, value);
        }

        public bool ValidateDiveStep(IDiveStepModel diveStep)
        {
            return ValidateDiveStep(diveStep.Depth, diveStep.Time);
        }

        private static bool ValidateDiveStep(int depth, int time)
        {
            return depth <= 100 && depth >= 0 && time <= 60 && time >= 1;
        }

        public IDiveStepModel DeepClone()
        {
            return new DiveStepViewModel()
            {
                Depth = Depth,
                Time = Time,
            };
        }
    }
}