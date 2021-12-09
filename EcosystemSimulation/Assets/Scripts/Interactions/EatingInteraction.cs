using Interactions;
using UnityEngine;

public class EatingInteraction : Interaction
{
    [SerializeField] private float hungerChangeFactor;
    private Needs _needs;
    
    protected override void Start()
    {
        base.Start();
        _needs = SimulationObject.GetComponent<Needs>();
    }
    protected override void AtInteractionEnd()
    {
        _needs["Hunger"] -= hungerChangeFactor;    //todo magic number - change
        Destroy(SecondSimulationObject);
        base.AtInteractionEnd();
    }
}