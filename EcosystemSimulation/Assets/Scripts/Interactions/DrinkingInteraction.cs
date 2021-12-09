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
        Debug.Log($"Interaction end");
        //_needs["Hunger"] -= hungerChangeFactor;    //todo magic number - change
        Destroy(SecondSimulationObject);
        base.AtInteractionEnd();
    }
}