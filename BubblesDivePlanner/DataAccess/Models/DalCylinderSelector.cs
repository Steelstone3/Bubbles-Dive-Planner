// TODO AH Test
public class DalCylinderSelector
{
    public DalCylinder[] Cylinders { get; set; }
    public DalCylinder SetupCylinder { get; set; }
    public DalCylinder SelectedCylinder { get; set; }
}

public class DalCylinder
{
    public string Name { get; set; }
    public byte Volume { get; set; }
    public ushort Pressure { get; set; }
    public ushort InitialPressurisedVolume { get; set; }
    public DalGasMixture GasMixture { get; set; }
    public DalGasUsage GasUsage { get; set; }
}

public class DalGasMixture
{
    public float Oxygen { get; set; }
    public float Helium { get; set; }
}

public class DalGasUsage
{
    public ushort Remaining { get; set; }
    public ushort Used { get; set; }
    public byte SurfaceAirConsumptionRate { get; set; }
}