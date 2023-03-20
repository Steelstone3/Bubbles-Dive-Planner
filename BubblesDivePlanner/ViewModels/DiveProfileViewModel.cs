using System.Linq;
using BubblesDivePlanner.Controllers;
using ReactiveUI;

namespace BubblesDivePlanner.DiveModels.DiveProfile
{
    public class DiveProfileViewModel : ReactiveObject, IDiveProfileModel
    {
        private readonly int _compartmentCount;

        public DiveProfileViewModel(int compartmentCount)
        {
            _compartmentCount = compartmentCount;
            ConstructDiveProfile();
            InitialiseDiveProfile();
        }

        private double[] _maxSurfacePressures;
        public double[] MaxSurfacePressures
        {
            get => _maxSurfacePressures;
            set => this.RaiseAndSetIfChanged(ref _maxSurfacePressures, value);
        }

        private double[] _tissuePressuresNitrogen;
        public double[] TissuePressuresNitrogen
        {
            get => _tissuePressuresNitrogen;
            set => this.RaiseAndSetIfChanged(ref _tissuePressuresNitrogen, value);
        }

        private double[] _tissuePressuresHelium;
        public double[] TissuePressuresHelium
        {
            get => _tissuePressuresHelium;
            set => this.RaiseAndSetIfChanged(ref _tissuePressuresHelium, value);
        }

        private double[] _tissuePressuresTotal;
        public double[] TissuePressuresTotal
        {
            get => _tissuePressuresTotal;
            set => this.RaiseAndSetIfChanged(ref _tissuePressuresTotal, value);
        }

        private double[] _toleratedAmbientPressures;
        public double[] ToleratedAmbientPressures
        {
            get => _toleratedAmbientPressures;
            set => this.RaiseAndSetIfChanged(ref _toleratedAmbientPressures, value);
        }

        private double[] _aValues;
        public double[] AValues
        {
            get => _aValues;
            set => this.RaiseAndSetIfChanged(ref _aValues, value);
        }

        public double[] _bValues;
        public double[] BValues
        {
            get => _bValues;
            set => this.RaiseAndSetIfChanged(ref _bValues, value);
        }

        public double[] _compartmentLoad;
        public double[] CompartmentLoad
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

        public double DiveCeiling => ToleratedAmbientPressures.Max() <= 0 ? 0 : DiveCeilingController.CalculateDiveCeiling(ToleratedAmbientPressures);

        public IDiveProfileModel DeepClone()
        {
            var deepCloneDiveProfile = new DiveProfileViewModel(_compartmentCount)
            {
                PressureOxygen = this.PressureOxygen,
                PressureHelium = this.PressureHelium,
                PressureNitrogen = this.PressureNitrogen,
            };

            MaxSurfacePressures.CopyTo(deepCloneDiveProfile.MaxSurfacePressures, 0);
            TissuePressuresNitrogen.CopyTo(deepCloneDiveProfile.TissuePressuresNitrogen, 0);
            TissuePressuresHelium.CopyTo(deepCloneDiveProfile.TissuePressuresHelium, 0);
            TissuePressuresTotal.CopyTo(deepCloneDiveProfile.TissuePressuresTotal, 0);
            ToleratedAmbientPressures.CopyTo(deepCloneDiveProfile.ToleratedAmbientPressures, 0);
            AValues.CopyTo(deepCloneDiveProfile.AValues, 0);
            BValues.CopyTo(deepCloneDiveProfile.BValues, 0);
            CompartmentLoad.CopyTo(deepCloneDiveProfile.CompartmentLoad, 0);

            return deepCloneDiveProfile;
        }

        private void ConstructDiveProfile()
        {
            _maxSurfacePressures = new double[_compartmentCount];
            _tissuePressuresNitrogen = new double[_compartmentCount];
            _tissuePressuresHelium = new double[_compartmentCount];
            _tissuePressuresTotal = new double[_compartmentCount];
            _toleratedAmbientPressures = new double[_compartmentCount];
            _aValues = new double[_compartmentCount];
            _bValues = new double[_compartmentCount];
            _compartmentLoad = new double[_compartmentCount];
        }

        private void InitialiseDiveProfile()
        {
            for (int compartment = 0; compartment < _compartmentCount; compartment++)
            {
                _maxSurfacePressures[compartment] = 0;
                _toleratedAmbientPressures[compartment] = 0;
                _compartmentLoad[compartment] = 0;
                _tissuePressuresNitrogen[compartment] = 0.79;
                _tissuePressuresHelium[compartment] = 0;
                _tissuePressuresTotal[compartment] = 0.79;
                _aValues[compartment] = 0;
                _bValues[compartment] = 0;
            }
        }
    }
}