using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.Results
{
    public class ResultViewModel : ReactiveObject, IResultModel
    {
        private IDiveStepModel _diveStepModel;
        public IDiveStepModel DiveStepModel
        {
            get => _diveStepModel;
            set => this.RaiseAndSetIfChanged(ref _diveStepModel, value);
        }

        private IDiveProfileModel _diveProfileModel;
        public IDiveProfileModel DiveProfileModel
        {
            get => _diveProfileModel;
            set => this.RaiseAndSetIfChanged(ref _diveProfileModel, value);
        }

        public IResultsHistoryModel ResultsHistoryModel { get; } = new ResultsHistoryViewModel();
    }
}