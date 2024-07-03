public class CentralNervousSystemToxicity : ICentralNervousSystemToxicity
{
    private const int ROWS = 11;
    public float[] OxygenPartialPressureConstant => new float[ROWS] { 1.6F, 1.5F, 1.4F, 1.3F, 1.2F, 1.1F, 1.0F, 0.9F, 0.8F, 0.7F, 0.6F };
    public ushort[] MaximumSingleDiveDuration => new ushort[ROWS] { 45, 120, 150, 180, 210, 240, 300, 360, 450, 570, 720 };
    public ushort[] TotalDailyDuration => new ushort[ROWS] { 150, 180, 180, 210, 240, 270, 300, 360, 450, 570, 720 };
}

public interface ICentralNervousSystemToxicity
{
    public float[] OxygenPartialPressureConstant { get; }
    public ushort[] MaximumSingleDiveDuration { get; }
    public ushort[] TotalDailyDuration { get; }
}