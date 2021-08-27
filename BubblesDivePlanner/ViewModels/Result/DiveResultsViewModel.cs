using System.Collections.ObjectModel;
using BubblesDivePlanner.Contracts.Models.Results;
using BubblesDivePlanner.Contracts.ViewModels.Results;
using BubblesDivePlanner.Models.Results;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Result
{
    public class DiveResultsViewModel : ViewModelBase, IDiveResultsViewModel
    {
        public ObservableCollection<IDiveResultsStepOutputModel> DiveProfileResults
        {
            get;
        } = new ObservableCollection<IDiveResultsStepOutputModel>();

        private IDiveParametersResultViewModel _diveParametersResult = new DiveParametersResultViewModel();
        public IDiveParametersResultViewModel DiveParametersResult
        {
            get => _diveParametersResult;
            set => this.RaiseAndSetIfChanged(ref _diveParametersResult, value);
        }
    }
}

