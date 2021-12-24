using System;
using System.Threading;
using Interactions;
using UnityEngine;

public class DrinkingInteraction : Interaction
{
    [SerializeField] private float thirstChangeFactor;
    private Needs _needs;
    
    protected override void Start()
    {
        base.Start();
        _needs = SimulationObject.GetComponent<Needs>();
    }

    protected override void AtInteractionIncrement()
    {
        Debug.Log($"Drinking");
        _needs["Thirst"] -= thirstChangeFactor;
        if (_needs["Thirst"] <= 0)
        {
            Interrupt();
        }
    }
}