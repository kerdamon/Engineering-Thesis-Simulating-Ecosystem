using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using Unity.MLAgents;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class ModelSwapper : MonoBehaviour
{
    [SerializeField] private NNModel eatingCarrotModel;

    private Agent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<MovementAgent>();
    }

    private void Update()
    {
        _agent.SetModel("SimulationAgentMovement", eatingCarrotModel);
    }
}
