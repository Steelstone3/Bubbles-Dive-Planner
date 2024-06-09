public class CylinderController : ICylinderController
{
    public ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure) => (ushort)(volume * pressure);

    public float CalculateNitrogen(float oxygen, float helium) => 100.0F - oxygen - helium;
}

public interface ICylinderController
{
    ushort CalculateInitialPressurisedVolume(byte volume, ushort pressure);
    float CalculateNitrogen(float oxygen, float helium);
}