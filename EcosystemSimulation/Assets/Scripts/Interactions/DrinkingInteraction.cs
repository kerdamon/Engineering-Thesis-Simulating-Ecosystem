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
    protected override void AtInteractionEnd()
    {
        _needs["Thirst"] -= thirstChangeFactor;    //todo magic number - change
    }
}