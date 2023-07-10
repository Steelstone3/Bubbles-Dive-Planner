using BubblesDivePlanner.ViewModels.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class Cylinder : ReactiveObject, ICylinder
    {
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
            set => this.RaiseAndSetIfChanged(ref initialPressurisedVolume, value);
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

        private ushort CalculatePressurisedVolume() => (ushort)(Volume * Pressure);
    }
}