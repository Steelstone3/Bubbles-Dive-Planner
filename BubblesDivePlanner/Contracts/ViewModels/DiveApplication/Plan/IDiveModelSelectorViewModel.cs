using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Models.DiveModels;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IDiveModelSelectorViewModel : IVisibility
    {
        List<IDiveModel> DiveModels { get; }

        IDiveModel SelectedDiveModel { get; set; }

        bool ValidateSelectedDiveModel(IDiveModel selectedDiveModel);
        
        bool IsReadOnlyUiVisible { get; set; }
    }
}