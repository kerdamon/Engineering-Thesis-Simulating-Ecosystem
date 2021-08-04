using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class MatingInteractor : Interactor
{

    protected override void StartInteraction(GameObject actor1, GameObject actor2)
    {
        Debug.Log($"Start mating");
    }

    protected override void EndInteraction(GameObject actor1, GameObject actor2)
    {
        Dictionary<string, float> offspringFeatures = new Dictionary<string, float>();
        foreach (var f in actor1.GetComponent<Features>().FeatureDictionary)
        {
            offspringFeatures[f.Key] = (f.Value + actor2.GetComponent<Features>().FeatureDictionary[f.Key]) / 2;
        }

        Debug.Log($"Spawning 1 offspring with features:");
        foreach (var offspringFeature in offspringFeatures)
        {
            Debug.Log($"{offspringFeature.Key}: {offspringFeature.Value}");
        }
    }

    protected override void WaitingIncrement(float percentageCompleted)
    {
        Debug.Log($"PercentageCompleted: {percentageCompleted}");
    }
}
