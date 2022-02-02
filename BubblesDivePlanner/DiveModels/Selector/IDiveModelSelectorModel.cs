using System.Collections.Generic;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.DiveModels.Selector
{
    public interface IDiveModelSelectorModel : IVisibility
    {
        IList<IDiveModel> DiveModels { get; }
        IDiveModel SelectedDiveModel { get; set; }
    }
}