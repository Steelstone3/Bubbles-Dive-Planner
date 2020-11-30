namespace DivePlannerMk3.Contracts
{
    public interface IGasUsageModel
    {
        int GasUsedForStep
        {
            get; set;
        }

        int GasRemaining
        {
            get; set;
        }
    }
}