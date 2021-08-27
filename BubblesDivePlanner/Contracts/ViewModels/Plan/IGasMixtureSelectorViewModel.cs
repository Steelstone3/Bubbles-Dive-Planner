using System;
using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.Contracts.ViewModels.Plan
{
    //TODO AH need to do this for the rest
    public interface IGasMixtureSelectorViewModel
    {
        double MaximumOperatingDepth { get; set; }
        
        IGasMixtureModel SelectedGasMixture { get; set; }

        ObservableCollection<IGasMixtureModel> GasMixtures { get; }
        
        GasMixtureViewModel NewGasMixture { get; set; }
        
        IObservable<bool> CanAddGasMixture { get; }

        ReactiveCommand<Unit, Unit> AddGasMixtureCommand { get; }

        bool ValidateGasMixture(IGasMixtureModel selectedGasMixture);
    }
}