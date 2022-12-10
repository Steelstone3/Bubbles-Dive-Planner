using System.Collections.Generic;
using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers.Json
{
    public interface IJsonController
    {
        string Serialise(List<IDivePlan> divePlans);
        IDivePlan Deserialise(string expectedDivePlanJson);
    }
}