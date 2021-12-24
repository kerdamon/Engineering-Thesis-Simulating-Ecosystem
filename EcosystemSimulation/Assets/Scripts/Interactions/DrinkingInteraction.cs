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
        if (_needs["Thirst"] - thirstChangeFactor < 0)
        {
            Interrupt();
        }
        else
        {
            _needs["Thirst"] -= thirstChangeFactor;
        }
    }
}