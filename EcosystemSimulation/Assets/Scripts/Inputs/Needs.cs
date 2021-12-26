using System;
using System.Collections.Generic;
using Input;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class Needs : DictionarySerializer<float>
{
    [SerializeField] private float increaseRate;
    [SerializeField] private float maxNeedValue;

    private MovementAgent _movementAgent;
    private Features _features;
    
    private void Start()
    {
        _features = GetComponent<Features>();
        _movementAgent = GetComponent<MovementAgent>();
        _movementAgent.AfterAction += UpdateNeeds;
    }

    private void UpdateNeeds()
    {
        IncreaseAndKillIfMax("Hunger");
        IncreaseAndKillIfMax("Thirst");
        IncreaseNeedUpToMax("ReproductionUrge");
    }

    private void IncreaseAndKillIfMax(string need)
    {
        IncreaseNeedUpToMax(need);
        const float epsilon = 0.0001f;
        if (this[need] >= maxNeedValue - epsilon)
        {
            _movementAgent.KillAgent(need);
        }
    }

    private void IncreaseNeedUpToMax(string need)
    {
        var increaseAmount = Time.deltaTime * increaseRate;
        increaseAmount += increaseAmount * (_features.CurrentGeneticCost / _features.MaxGeneticCost);
        this[need] += increaseAmount;
        if (this[need] >= maxNeedValue)
        {
            this[need] = maxNeedValue;
        }
    }
}
