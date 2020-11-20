namespace DivePlannerMk3.Contracts
{
    public interface IGasManagementInfoModel
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