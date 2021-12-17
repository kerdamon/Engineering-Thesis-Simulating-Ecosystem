using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class FoxMovementAgent : MovementAgent
{
    private float fox_each_step_fixed;
    public override void Initialize()
    {
        base.Initialize();
        fox_each_step_fixed = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_eating_rabbit_reward", 0.0f);
        Debug.Log($"fox_each_step_fixed = {fox_each_step_fixed}");
    }

    protected override void ModifyRewardOnActionReceived()
    {
        AddReward(fox_each_step_fixed / MaxStep);
    }
}