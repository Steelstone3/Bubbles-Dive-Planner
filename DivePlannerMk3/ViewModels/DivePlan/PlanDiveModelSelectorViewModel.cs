using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using System.Linq;
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

        //TODO This solution is temporary it is ok to have a default if the UI lines up
        private IDiveModel _selectedDiveModel = new Zhl16Buhlmann();
        public IDiveModel SelectedDiveModel
        {
            get => _selectedDiveModel;
            set
            {
                //TODO Get this code here**
                _selectedDiveModel = value;
                _diveProfileController.TheDiveModel = _selectedDiveModel;
                
                //is this the answer? No
                //this.RaisePropertyChanged(nameof(SelectedDiveModel));

                //is this the answer?
                this.RaiseAndSetIfChanged( ref _selectedDiveModel, value );
            }
        }

        public PlanDiveModelSelectorViewModel(IDiveProfileService diveProfileController)
        {
            _diveProfileController = diveProfileController;
        }
    }
}