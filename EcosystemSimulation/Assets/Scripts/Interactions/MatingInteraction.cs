using Interactions;
using UnityEngine;

public class MatingInteraction : Interaction
{
    protected override void AtInteractionStart()
    {
    }

    protected override void AtInteractionEnd()
    {
        SpawnOffspring(SecondSimulationObject);
    }

    private void SpawnOffspring(GameObject mate)
    {
        var offspring = Instantiate(gameObject, transform);
        offspring.transform.Translate(Random.value, 0, Random.value);
        var offspringFeatures = offspring.GetComponent<Features>();
        var actorFeatures = SimulationObject.GetComponent<Features>();
        var mateFeatures = mate.GetComponent<Features>(); 
        foreach (var f in actorFeatures)
        {
            offspringFeatures[f.Key] = (f.Value + mateFeatures[f.Key]) / 2;
        }
    }
}