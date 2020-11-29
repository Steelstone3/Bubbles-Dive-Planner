using System.Collections.ObjectModel;
using DivePlannerMk3.Models;

namespace DivePlannerMk3.ViewModels.DiveResult
{
    public class DiveResultsViewModel
    {
        public ObservableCollection<DiveResultsModel> DiveProfileResults
        {
            get;
        } = new ObservableCollection<DiveResultsModel>();
    }
}

