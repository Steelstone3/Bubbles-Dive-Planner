using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveStagesController
    {
        IDivePlan Run(IDivePlan divePlan);
    }
}