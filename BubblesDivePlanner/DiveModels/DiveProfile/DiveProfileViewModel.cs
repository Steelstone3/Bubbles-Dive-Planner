using System.Collections.Generic;
using ReactiveUI;

namespace BubblesDivePlanner.DiveModels.DiveProfile
{
    public class DiveProfileViewModel : ReactiveObject, IDiveProfileModel
    {
        private IList<double> _maxSurfacePressures = new List<double>();
        public IList<double> MaxSurfacePressures
        {
            get => _maxSurfacePressures;
            set => this.RaiseAndSetIfChanged(ref _maxSurfacePressures, value);
        }

        private IList<double> _tissuePressuresNitrogen = new List<double>();
        public IList<double> TissuePressuresNitrogen
        {
            get => _tissuePressuresNitrogen;
            set => this.RaiseAndSetIfChanged(ref _tissuePressuresNitrogen, value);
        }

        private IList<double> _tissuePressuresHelium = new List<double>();
        public IList<double> TissuePressuresHelium
        {
            get => _tissuePressuresHelium;
            set => this.RaiseAndSetIfChanged(ref _tissuePressuresHelium, value);
        }

        private IList<double> _tissuePressuresTotal = new List<double>();
        public IList<double> TissuePressuresTotal
        {
            get => _tissuePressuresTotal;
            set => this.RaiseAndSetIfChanged(ref _tissuePressuresTotal, value);
        }

        private IList<double> _toleratedAmbientPressures = new List<double>();
        public IList<double> ToleratedAmbientPressures
        {
            get => _toleratedAmbientPressures;
            set => this.RaiseAndSetIfChanged(ref _toleratedAmbientPressures, value);
        }

        private IList<double> _aValues = new List<double>();
        public IList<double> AValues
        {
            get => _aValues;
            set => this.RaiseAndSetIfChanged(ref _aValues, value);
        }

        public IList<double> _bValues = new List<double>();
        public IList<double> BValues
        {
            get => _bValues;
            set => this.RaiseAndSetIfChanged(ref _bValues, value);
        }

        public IList<double> _compartmentLoad = new List<double>();
        public IList<double> CompartmentLoad
        {
            get => _compartmentLoad;
            set => this.RaiseAndSetIfChanged(ref _compartmentLoad, value);
        }

        public double _pressureOxygen = 0.0;
        public double PressureOxygen
        {
            get => _pressureOxygen;
            set => this.RaiseAndSetIfChanged(ref _pressureOxygen, value);
        }

        public double _pressureHelium = 0.0;
        public double PressureHelium
        {
            get => _pressureHelium;
            set => this.RaiseAndSetIfChanged(ref _pressureHelium, value);
        }

        public double _pressureNitrogen = 0.0;
        public double PressureNitrogen
        {
            get => _pressureNitrogen;
            set => this.RaiseAndSetIfChanged(ref _pressureNitrogen, value);
        }

        public IDiveProfileModel DeepClone()
        {
            return new DiveProfileViewModel()
            {
                MaxSurfacePressures = new List<double>(this.MaxSurfacePressures),
                TissuePressuresNitrogen = new List<double>(this.TissuePressuresNitrogen),
                TissuePressuresHelium = new List<double>(this.TissuePressuresHelium),
                TissuePressuresTotal = new List<double>(this.TissuePressuresTotal),
                ToleratedAmbientPressures = new List<double>(this.ToleratedAmbientPressures),
                AValues = new List<double>(this.AValues),
                BValues = new List<double>(this.BValues),
                CompartmentLoad = new List<double>(this.CompartmentLoad),
                PressureOxygen = this.PressureOxygen,
                PressureHelium = this.PressureHelium,
                PressureNitrogen = this.PressureNitrogen,
            };
        }
    }
}