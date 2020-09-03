using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using System.Linq;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanDiveModelSelectorViewModel : ViewModelBase
    {
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

                var diveProfileController = new DiveProfileService();
                diveProfileController.TheDiveModel = SelectedDiveModel;
            }
        }
    }
}