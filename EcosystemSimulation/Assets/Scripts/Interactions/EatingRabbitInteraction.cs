using Interactions;
using UnityEngine;

public class EatingRabbitInteraction : Interaction
{
    private Needs _needs;
    
    protected override void Start()
    {
        base.Start();
        _needs = SimulationObject.GetComponent<Needs>();
    }
    protected override void AtInteractionEnd()
    {
        var rabbitInteractionManager = SecondSimulationObject.GetComponentInChildren<RabbitInteractionManager>();
        var energyReceived = rabbitInteractionManager.OnEaten();
        _needs["Hunger"] -= energyReceived;
    }
}