using System;
using System.Collections.Generic;
using Input;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class Needs : DictionarySerializer<float>
{
    [SerializeField] private float increaseRate;
    [SerializeField] private float maxNeed;

    private MovementAgent _movementAgent;
    
    private void Start()
    {
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
        this[need] += Time.deltaTime * increaseRate; 
        if (this[need] >= 100.0f)
        {
            _movementAgent.KillAgent(need);
        }
    }

    private void IncreaseNeedUpToMax(string need)
    {
        this[need] += Time.deltaTime * increaseRate; 
        if (this[need] >= 100.0f)
        {
            this[need] = 100.0f;
        }
    }
}
