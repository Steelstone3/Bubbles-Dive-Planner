using ReactiveUI;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
           
        }

        public IDiveStepModel DiveStep {get; set;} = new DiveStepViewModel();
    }
}
