// TODO AH Test
public class DalCylinder
{
    public string Name { get; set; }
    public byte Volume { get; set; }
    public ushort Pressure { get; set; }
    public ushort InitialPressurisedVolume { get; set; }
    public DalGasMixture GasMixture { get; set; }
    public DalGasUsage GasUsage { get; set; }
}