using Unity.MLAgents;

public class RabbitMovementAgent : MovementAgent
{
    private float rabbit_each_episode_fixed;
    public override void Initialize()
    {
        base.Initialize();
        rabbit_each_episode_fixed = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_each_episode_fixed ", 0.0f);
    }

    protected override void ModifyRewardOnActionReceived()
    {
        AddReward(rabbit_each_episode_fixed / MaxStep);
    }
}
