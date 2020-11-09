using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanDiveModelSelectorViewModel : ViewModelBase
    {
        private IDiveProfileService _diveProfileController;

        public List<IDiveModel> DiveModels => new List<IDiveModel>
        {
            new Zhl16Buhlmann(),
        };

        private IDiveModel _selectedDiveModel;
        public IDiveModel SelectedDiveModel
        {
            get => _selectedDiveModel;
            set
            {
                _selectedDiveModel = value;
                _diveProfileController.TheDiveModel = _selectedDiveModel;

                this.RaisePropertyChanged(nameof(SelectedDiveModel));
            }
        }

        public PlanDiveModelSelectorViewModel(IDiveProfileService diveProfileController)
        {
            _diveProfileController = diveProfileController;
        }
    }
}