using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Entities;
using BubblesDivePlanner.Contracts.Models.DiveModels;

namespace BubblesDivePlanner.Entities
{
    public class DivePlanEntityModel : IEntityModel
    {
        public int CylinderPressure { get; set; }
        public int Depth { get; set; }
        public int Time { get; set; }
        public int CylinderVolume { get; set; }
        public int GasRemaining { get; set; }
        public int GasUsedForStep { get; set; }
        public int InitialCylinderTotalVolume { get; set; }
        public int SacRate { get; set; }
        public IDiveModel SelectedDiveModel { get; set; }
        public double MaximumOperatingDepth { get; set; }
        public string NewGasMixtureGasName { get; set; } = string.Empty;
        public double NewGasMixtureHelium { get; set; }
        public double NewGasMixtureNitrogen { get; set; }
        public double NewGasMixtureOxygen { get; set; }
        public string SelectedGasMixtureGasName { get; set; }
        public double SelectedGasMixtureHelium { get; set; }
        public double SelectedGasMixtureNitrogen { get; set; }
        public double SelectedGasMixtureOxygen { get; set; }
        public List<string> GasName { get; set; } = new List<string>();
        public List<double> Helium { get; set; } = new List<double>();
        public List<double> Nitrogen { get; set; } = new List<double>();
        public List<double> Oxygen { get; set; } = new List<double>();
    }
}