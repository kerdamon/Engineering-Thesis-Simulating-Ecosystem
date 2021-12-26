using System;
using UnityEngine;

public class Needs : DictionarySerializer<float>
{
    [SerializeField] private float increaseRate;

    private MovementAgent _movementAgent;
    private Features _features;
    
    private const float TOLERANCE = 0.0001f;  //constant for precision in floating point numbers equality checks
    
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
        if (IsMaxOrGreater(need))
        {
            _movementAgent.KillAgent(need);
        }
    }

    private void IncreaseNeedUpToMax(string need)
    {
        var increaseAmount = Time.deltaTime * increaseRate;
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
}
