using ReactiveUI;

namespace BubblesDivePlanner.DiveStep
{
    public class DiveStepViewModel : ReactiveObject, IDiveStepModel
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

        public bool ValidateDiveStep(IDiveStepModel diveStep)
        {
            return ValidateDiveStep(diveStep.Depth, diveStep.Time);
        }

        private bool ValidateDiveStep(int depth, int time)
        {
            return depth <= 100 && depth >= 0 && time <= 60 && time >= 1;
        }
    }
}