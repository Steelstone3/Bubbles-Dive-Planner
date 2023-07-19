using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Cylinders
{
    public class Cylinder : ViewModelBase, ICylinder
    {
        public Cylinder() { }

        public Cylinder(ICylinder cylinder)
        {
            Name = cylinder.Name;
            Volume = cylinder.Volume;
            Pressure = cylinder.Pressure;
            GasMixture = cylinder.GasMixture;
            GasUsage = cylinder.GasUsage;
        }

        private string name = string.Empty;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private ushort initialPressurisedVolume;
        public ushort InitialPressurisedVolume
        {
            get => initialPressurisedVolume;
            private set
            {
                this.RaiseAndSetIfChanged(ref initialPressurisedVolume, value);
                GasUsage.GasRemaining = InitialPressurisedVolume;
            }
        }

        private ushort volume;
        public ushort Volume
        {
            get => volume;
            set
            {
                this.RaiseAndSetIfChanged(ref volume, value);
                InitialPressurisedVolume = CalculatePressurisedVolume();
            }
        }

        private ushort pressure;
        public ushort Pressure
        {
            get => pressure;
            set
            {
                this.RaiseAndSetIfChanged(ref pressure, value);
                InitialPressurisedVolume = (ushort)(Volume * Pressure);
            }
        }

        private IGasMixture gasMixture = new GasMixture();
        public IGasMixture GasMixture
        {
            get => gasMixture;
            set => this.RaiseAndSetIfChanged(ref gasMixture, value);
        }

        private IGasUsage gasUsage = new GasUsage();
        public IGasUsage GasUsage
        {
            get => gasUsage;
            set => this.RaiseAndSetIfChanged(ref gasUsage, value);
        }

        // TODO AH Move to a controller
        private ushort CalculatePressurisedVolume() => (ushort)(Volume * Pressure);
    }
}