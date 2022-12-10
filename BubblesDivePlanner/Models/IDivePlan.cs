using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Models
{
    public interface IDivePlan
    {
        IDiveModel DiveModel { get; }
        List<ICylinder> Cylinders { get; }
        IDiveStep DiveStep { get; }
        ICylinder SelectedCylinder { get; }
    }
}