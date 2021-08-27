using System;
using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Contracts.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IGasMixtureSelectorViewModel
    {
        double MaximumOperatingDepth { get; set; }
        
        IGasMixtureModel SelectedGasMixture { get; set; }

        ObservableCollection<IGasMixtureModel> GasMixtures { get; }
        
        IGasMixtureViewModel NewGasMixture { get; set; }
        
        IObservable<bool> CanAddGasMixture { get; }

        ReactiveCommand<Unit, Unit> AddGasMixtureCommand { get; }

        bool ValidateGasMixture(IGasMixtureModel selectedGasMixture);
    }
}