using System.Collections.ObjectModel;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveResult
{
    public class DiveResultsViewModel : ViewModelBase, IModelConverter
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

        public void ModelToEntity()
        {
            throw new System.NotImplementedException();
        }

        public void EntityToModel()
        {
            throw new System.NotImplementedException();
        }
    }
}

