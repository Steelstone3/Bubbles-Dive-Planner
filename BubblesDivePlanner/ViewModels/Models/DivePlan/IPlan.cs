namespace BubblesDivePlanner.ViewModels.Models.DivePlan
{
    public interface IPlanner
    {
        // IDiveModel SelectedDiveModel { get; set; }
        // ICylinder SelectedCylinder { get; set; }
        IDiveStep DiveStep { get; set; }
    }
}