using Interactions;
using UnityEngine;

public class EatingInteractor : Interactor
{
    private Needs _needs;

    private void Start()
    {
        _needs = GetComponent<Needs>();
    }

    protected override void AtInteractionStart(GameObject actor, GameObject food)
    {
        Debug.Log($"Start Eating");
    }

    protected override void AtInteractionEnd(GameObject actor, GameObject food)
    {
        _needs["Hunger"] -= 30f;    //todo magic number - change
        Destroy(food);
    }

    protected override void AtInteractionIncrement(float percentageCompleted)
    {
        Debug.Log($"PercentageCompleted: {percentageCompleted}");
    }
}
