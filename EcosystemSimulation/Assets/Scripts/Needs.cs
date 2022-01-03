using System;
using Unity.MLAgents;
using UnityEngine;

public class Needs : DictionarySerializer<float>
{
    [SerializeField] private float increaseRate;

    private MovementAgent _movementAgent;
    private Features _features;
    
    private const float TOLERANCE = 0.0001f;  //constant for precision in floating point numbers equality checks

    private bool is_training;
    
    private void Start()
    {
        is_training = Academy.Instance.EnvironmentParameters.GetWithDefault("is_training", 0) > 0;
        _features = GetComponent<Features>();
        _movementAgent = GetComponent<MovementAgent>();
        _movementAgent.AfterAction += UpdateNeeds;
    }

    private void UpdateNeeds()
    {
        IncreaseAndKillIfMax("Hunger", 1.0f);
        IncreaseAndKillIfMax("Thirst", 1.0f);
        var reproductionUrgeModifier = CalculateReproductionUrgeModifier();
        IncreaseNeedUpToMax("ReproductionUrge", reproductionUrgeModifier);
    }

    private void IncreaseAndKillIfMax(string need, float modifier)
    {
        IncreaseNeedUpToMax(need, modifier);
        if (IsMaxOrGreater(need))
        {
            if (!is_training)
            {
                _movementAgent.KillAgent(need);
            }
        }
    }

    private void IncreaseNeedUpToMax(string need, float modifier)
    {
        var increaseAmount = Time.deltaTime * increaseRate * modifier;
        increaseAmount += increaseAmount * (_features.CurrentGeneticCost / _features.MaxGeneticCost);
        this[need] += increaseAmount;
        if (IsMaxOrGreater(need))
        {
            this[need] = maxValue;
        }
    }

    public override bool IsMaxOrGreater(string feature)
    {
        return this[feature] >= maxValue - TOLERANCE;
    }

    private float CalculateReproductionUrgeModifier()
    {
        return _features["Fertility"] * 2.0f / 500.0f + 0.8f;
    }
}
