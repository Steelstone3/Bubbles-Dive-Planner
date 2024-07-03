using Tmds.DBus.Protocol;

public class DecompressionController
{
    public List<IDiveStep> CollateDecompressionDiveSteps(IDiveStage diveStage)
    {
        List<IDiveStep> diveStepModelQueue = new();

        while (diveStage.DiveModel.DiveModelProfile.DiveCeiling > 0)
        {
            diveStepModelQueue.Add(CalculateDecompressionSteps(diveStage));
        }

        return diveStepModelQueue;
    }

    private IDiveStep CalculateDecompressionSteps(IDiveStage diveStage)
    {
        float diveCeiling = diveStage.DiveModel.DiveModelProfile.DiveCeiling;

        if (diveCeiling <= 0.0F)
        {
            return null;
        }

        IDiveStep diveStep = GetNearestDepthToStepInterval(diveCeiling);
        diveStep = RunDecompressionSimulation(new DiveStage(new DiveStageValidator())
        {
            DiveModel = diveStage.DiveModel,
            DiveStep = diveStep,
            Cylinder = diveStage.Cylinder
        });

        return diveStep;
    }

    private IDiveStep GetNearestDepthToStepInterval(float diveCeiling)
    {
        var diveStepModel = new DiveStep(new DiveStepValidator())
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

    private IDiveStep RunDecompressionSimulation(IDiveStage diveStage)
    {
        DiveProfileStagesFactory diveProfileStagesFactory = new();

        byte time = 0;

        while (diveStage.DiveStep.Depth == FindNearestDepthToDiveCeiling(diveStage.DiveModel.DiveModelProfile.DiveCeiling))
        {
            time++;

            diveProfileStagesFactory.Run(diveStage);
        }

        return new DiveStep(new DiveStepValidator())
        {
            Depth = diveStage.DiveStep.Depth,
            Time = time
        };
    }
}

public interface IDecompressionController
{

}