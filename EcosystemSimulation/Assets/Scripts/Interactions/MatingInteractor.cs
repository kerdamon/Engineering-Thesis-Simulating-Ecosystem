using Interactions;
using UnityEngine;

public class MatingInteractor : Interactor
{
    protected override void AtInteractionStart(GameObject actor1, GameObject actor2)
    {
        Debug.Log($"Start mating");
    }

    protected override void AtInteractionEnd(GameObject actor1, GameObject actor2)
    {
        SpawnOffspring(actor1, actor2);
    }

    protected override void AtInteractionIncrement(float percentageCompleted)
    {
        Debug.Log($"PercentageCompleted: {percentageCompleted}");
    }

    private void SpawnOffspring(GameObject actor1, GameObject actor2)
    {
        var offspring = Instantiate(gameObject, transform);
        offspring.transform.Translate(Random.value, 0, Random.value);
        var offspringFeatures = offspring.GetComponent<Features>();
        var actor1Features = actor1.GetComponent<Features>();
        var actor2Features = actor2.GetComponent<Features>(); 
        foreach (var f in actor1Features)
        {
            offspringFeatures[f.Key] = (f.Value + actor2Features[f.Key]) / 2;
        }
    }
}
