using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class EatingInteractor : Interactor
{
    private Needs _needs;

    private void Start()
    {
        _needs = GetComponent<Needs>();
    }

    protected override void StartInteraction(GameObject actor, GameObject food)
    {
        Debug.Log($"Start Eating");
    }

    protected override void EndInteraction(GameObject actor, GameObject food)
    {
        _needs.NeedsDictionary["Hunger"] -= 10f;    //todo magic number - change
        Destroy(food);
    }

    protected override void WaitingIncrement(float percentageCompleted)
    {
        Debug.Log($"PercentageCompleted: {percentageCompleted}");
    }
}
