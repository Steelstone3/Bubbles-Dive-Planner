using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveController
    {
        IDivePlan RunDiveProfile(IDivePlan divePlan);
    }
}