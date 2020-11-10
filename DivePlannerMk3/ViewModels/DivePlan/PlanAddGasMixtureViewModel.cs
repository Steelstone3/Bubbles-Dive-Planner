using System;
using System.Collections.Generic;
using System.Reactive;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanAddGasMixtureViewModel : ViewModelBase
    {
        public PlanAddGasMixtureViewModel()
        {
            AddGasMixtureCommand = ReactiveCommand.Create( AddGasMixture, CanAddGasMixture );
        }

        private string _gasName ="Air";
        public string GasName
        {
            get => _gasName; 
            set
            {
                if(_gasName != value)
                {
                    _gasName = value;
                    this.RaisePropertyChanged(nameof(GasName));
                }
            }
        }

        private double _oxygen = 21;
        public double Oxygen
        {
            get => _oxygen;
            set
            {
                if (_oxygen != value)
                {
                    this.RaiseAndSetIfChanged(ref _oxygen, value);
                    this.RaisePropertyChanged(nameof(Nitrogen));
                }
            }
        }

        private double _helium = 0;
        public double Helium
        {
            get => _helium;
            set
            {
                if (_helium != value)
                {
                    this.RaiseAndSetIfChanged(ref _helium, value);
                    this.RaisePropertyChanged(nameof(Nitrogen));
                }
            }
        }

        public double Nitrogen => CalculateNitrogen();

        public IObservable<bool> CanAddGasMixture
        {
            get => this.WhenAnyValue(vm => vm.GasName, (gasName) => !string.IsNullOrEmpty(gasName));
        }

        public ReactiveCommand<Unit, Unit>  AddGasMixtureCommand
        {
            get;
        }

        private void AddGasMixture()
        {
            //Add to gas mixtures list
            var newGasMixture = new GasMixtureModel()
            {
                GasName = _gasName,
                Helium = _helium,
                Oxygen = _oxygen,
                Nitrogen = CalculateNitrogen(),
            };

            //TODO AH add to gas mixtures
            //GasMixtures.Add( newGasMixture );
        }

        private double CalculateNitrogen() => 100 - (_oxygen + _helium);
    }
}
