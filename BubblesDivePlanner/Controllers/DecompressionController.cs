public class DecompressionController
{
    public List<DiveStep> CollateDecompressionDiveSteps(DiveStage diveStage)
    {
        List<DiveStep> diveStepModelQueue = new();

        while (diveStage.DiveModel.DiveModelProfile.DiveCeiling > 0)
        {
            diveStepModelQueue.Add(CalculateDecompressionSteps(diveStage));
        }

        return diveStepModelQueue;
    }

    private DiveStep CalculateDecompressionSteps(DiveStage diveStage)
    {
        float diveCeiling = diveStage.DiveModel.DiveModelProfile.DiveCeiling;

        if (diveCeiling <= 0.0F)
        {
            return null;
        }

        DiveStep diveStep = GetNearestDepthToStepInterval(diveCeiling);
        diveStep = RunDecompressionSimulation(new DiveStage()
        {
            DiveModel = diveStage.DiveModel,
            DiveStep = diveStep,
            Cylinder = diveStage.Cylinder
        });

        return diveStep;
    }

    private DiveStep GetNearestDepthToStepInterval(float diveCeiling)
    {
        var diveStepModel = new DiveStep()
        {
            Depth = FindNearestDepthToDiveCeiling(diveCeiling),
            Time = 1
        };

        return diveStepModel;
    }

    private byte FindNearestDepthToDiveCeiling(float diveCeiling)
    {
        const int stepInterval = 3;
        return diveCeiling > 0 ? (byte)(Math.Ceiling(diveCeiling / stepInterval) * stepInterval) : (byte)0;
    }

    private DiveStep RunDecompressionSimulation(DiveStage diveStage)
    {
        DiveProfileStagesCommand diveProfileStagesCommand = new();

        byte time = 0;

        while (diveStage.DiveStep.Depth == FindNearestDepthToDiveCeiling(diveStage.DiveModel.DiveModelProfile.DiveCeiling))
        {
            time++;

            diveProfileStagesCommand.Run(diveStage);
        }

        return new DiveStep()
        {
            Depth = diveStage.DiveStep.Depth,
            Time = time
        };
    }
}

public interface IDecompressionController
{

}