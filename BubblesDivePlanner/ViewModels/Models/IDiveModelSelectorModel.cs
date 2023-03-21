using System.Collections.Generic;

namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IDiveModelSelectorModel : IVisibility
    {
        IList<IDiveModel> DiveModels { get; }
        IDiveModel SelectedDiveModel { get; set; }
        bool ValidateSelectedDiveModel(IDiveModel selectorDiveModel);
    }
}