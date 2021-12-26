using Unity.MLAgents;

public class FoxMovementAgent : MovementAgent
{
    private float fox_each_step_fixed;
    public override void Initialize()
    {
        base.Initialize();
        fox_each_step_fixed = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_eating_rabbit_reward", 0.0f);
    }

    protected override void ModifyRewardOnActionReceived()  //todo move adding reward to training settings to get rid shild classes and use MovementAgent for all agents
    {
        AddReward(fox_each_step_fixed / MaxStep);
    }
}
