using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class RabbitMovementAgent : MovementAgent
{
    private float rabbit_each_step_fixed;
    public override void Initialize()
    {
        base.Initialize();
        rabbit_each_step_fixed = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_eating_rabbit_reward", 0.0f);
    }

    protected override void ModifyRewardOnActionReceived()
    {
        AddReward(rabbit_each_step_fixed / MaxStep);
    }
}
