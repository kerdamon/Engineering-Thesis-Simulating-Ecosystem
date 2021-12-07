using Interactions;
using UnityEngine;

public class DrinkingInteraction : Interaction
{
    [SerializeField] private float thirstChangeFactor;
    private Needs _needs;
    
    protected override void Start()
    {
        base.Start();
        _needs = Actor.GetComponent<Needs>();
    }
    protected override void AtInteractionEnd()
    {
        Debug.Log($"Interaction end");
        //_needs["Hunger"] -= hungerChangeFactor;    //todo magic number - change
        Destroy(SecondActor);
        base.AtInteractionEnd();
    }
}