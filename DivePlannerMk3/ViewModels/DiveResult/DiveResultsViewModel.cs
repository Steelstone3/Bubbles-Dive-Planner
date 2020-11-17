using System.Collections.ObjectModel;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveResult
{
    public class DiveResultsViewModel : ViewModelBase
    {
        public ObservableCollection<DiveResultsModel> DiveProfileResults
        {
            get;
        } = new ObservableCollection<DiveResultsModel>();

	 	public IDiveParametersOutputModel DiveParametersUsed
        {
            get; set;
        }
    }
}

