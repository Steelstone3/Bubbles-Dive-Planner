using System.Collections.Generic;
using ReactiveUI;

namespace BubblesDivePlanner.DiveModels.Selector
{
    public class DiveModelSelectorViewModel : ReactiveObject, IDiveModelSelectorModel
    {
        public IList<IDiveModel> DiveModels => new List<IDiveModel>
        {
            new Zhl16BuhlmannModel(),
        };

        private IDiveModel _selectedDiveModel;
        public IDiveModel SelectedDiveModel
        {
            get => _selectedDiveModel;
            set => this.RaiseAndSetIfChanged(ref _selectedDiveModel, value);
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
    }
}