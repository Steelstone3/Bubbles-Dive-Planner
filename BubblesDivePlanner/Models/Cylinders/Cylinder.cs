namespace BubblesDivePlanner.Models.Cylinders
{
    public class Cylinder : ICylinder
    {
        public Cylinder(string name, ushort cylinderVolume, ushort cylinderPressure, byte surfaceAirConsumptionRate, IGasMixture gasMixture, ushort remainingGas, ushort usedGas)
        {
            Name = name;
            CylinderVolume = cylinderVolume;
            CylinderPressure = cylinderPressure;
            InitialPressurisedVolume = CalculateInitialPressurisedVolume();
            RemainingGas = remainingGas;
            RemainingGas = AssignRemainingGas(remainingGas);
            UsedGas = usedGas;
            SurfaceAirConsumptionRate = surfaceAirConsumptionRate;
            GasMixture = gasMixture;
        }

        public string Name { get; }
        public ushort CylinderVolume { get; }
        public ushort CylinderPressure { get; }
        public ushort InitialPressurisedVolume { get; private set; }
        public ushort RemainingGas { get; private set; }
        public ushort UsedGas { get; private set; }
        public byte SurfaceAirConsumptionRate { get; }
        public IGasMixture GasMixture { get; }

        public void UpdateCylinderGasConsumption(IDiveStep diveStep)
        {
            var depthPressure = diveStep.Depth != 0 ? ((diveStep.Depth / 10) + 1) : 0;
            UsedGas = diveStep.Time != 0 ? (ushort)(depthPressure * diveStep.Time * SurfaceAirConsumptionRate) : (ushort)0;
            RemainingGas = UsedGas < RemainingGas ? (ushort)(RemainingGas - UsedGas) : (ushort)0;
        }

        private ushort CalculateInitialPressurisedVolume()
        {
            return (ushort)(CylinderVolume * CylinderPressure);
        }

        private ushort AssignRemainingGas(ushort remainingGas)
        {
            return remainingGas switch
            {
                0 => InitialPressurisedVolume,
                _ => remainingGas,
            };
        }
    }
}