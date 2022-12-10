namespace BubblesDivePlanner.Models.Cylinders
{
    public interface ICylinder
    {
        string Name { get; }
        ushort CylinderVolume { get; }
        ushort CylinderPressure { get; }
        ushort InitialPressurisedVolume { get; }
        ushort RemainingGas { get; }
        ushort UsedGas { get; }
        byte SurfaceAirConsumptionRate { get; }
        IGasMixture GasMixture { get; }

        void UpdateCylinderGasConsumption(IDiveStep diveStep);
    }
}