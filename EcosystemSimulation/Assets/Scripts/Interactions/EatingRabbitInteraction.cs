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
        var _rabbitInteractionManager = SecondSimulationObject.GetComponent<RabbitInteractionManager>();
        var energy_received = _rabbitInteractionManager.OnEaten();
        _needs["Hunger"] -= energy_received;
        base.AtInteractionEnd();
    }
}