using System.Collections.ObjectModel;
using BubblesDivePlanner.Models.Results;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Result
{
    public class DiveResultsViewModel : ViewModelBase
    {
        public ObservableCollection<DiveResultsModel> DiveProfileResults
        {
            get;
        } = new ObservableCollection<DiveResultsModel>();

        private DiveParametersResultViewModel _diveParametersResult = new DiveParametersResultViewModel();
        public DiveParametersResultViewModel DiveParametersResult
        {
            get => _diveParametersResult;
            set => this.RaiseAndSetIfChanged(ref _diveParametersResult, value);
        }
    }
}

