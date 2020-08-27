using System.Collections.ObjectModel;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveResult
{
    /// <summary>
    /// Results to output on the main view
    /// </summary>
    public class DiveResultsViewModel : ViewModelBase
    {
        public DiveResultsViewModel()
        {
            DiveProfileResults = new DiveProfileResultsListViewModel();
        }

        private DiveProfileResultsListViewModel _diveProfileResults;
        public DiveProfileResultsListViewModel DiveProfileResults
        {
            get => _diveProfileResults;
            set => this.RaiseAndSetIfChanged( ref _diveProfileResults, value );
        }

        public ObservableCollection<DiveProfileResultsListViewModel> DiveProfileHistoryResults
        {
            get;
        } = new ObservableCollection<DiveProfileResultsListViewModel>();
    }
}