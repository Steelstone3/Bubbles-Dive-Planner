using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class DiveModelSelectorViewModel : ViewModelBase
    {
        private IDiveProfileService _diveProfileController;

        public DiveModelSelectorViewModel(IDiveProfileService diveProfileController)
        {
            IsUiVisible = true;
            _diveProfileController = diveProfileController;
        }

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
                if (_selectedDiveModel != value)
                {
                    _selectedDiveModel = value;
                    _diveProfileController.TheDiveModel = _selectedDiveModel;
                    this.RaisePropertyChanged(nameof(SelectedDiveModel));
                }
            }
        }
        
        private bool _isReadOnlyUiVisible = false;
        public bool IsReadOnlyUiVisible
        {
            get => _isReadOnlyUiVisible;
            set => this.RaiseAndSetIfChanged(ref _isReadOnlyUiVisible, value);
        }

        public bool ValidateSelectedDiveModel(IDiveModel selectedDiveModel)
        {
            return selectedDiveModel != null && _diveProfileController.TheDiveModel != null;
        }
    }
}