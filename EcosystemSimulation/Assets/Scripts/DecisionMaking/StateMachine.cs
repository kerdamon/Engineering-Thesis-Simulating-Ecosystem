using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Needs))]
public class StateMachine : MonoBehaviour
{
    public Action<Vector3> moveAction;  //todo change to joining references in editor
    public Action interactAction;

    private Needs _needs;

    private ActorState _currentState;
    
    private void Start()
    {
        _needs = GetComponent<Needs>();
    }

    private void Update()
    {
        InferState();
        ActOnState();

    }

    private void InferState()
    {
        _currentState = ActorState.Chilling;
        Debug.Log($"_needs.FeatureDictionary[Hunger] > 80 {_needs.NeedsDictionary["Hunger"] > 80}");
        if (_needs.NeedsDictionary["Hunger"] > 80)
        {
            _currentState = ActorState.LookingForFood;
        } 
    }

    private void ActOnState()
    {
        switch (_currentState)
        {
            case ActorState.LookingForFood:
                var closestFood = FindClosestFood();
                moveAction(closestFood.transform.position);
                break;
            case ActorState.Eating:
                break;
            case ActorState.LookingForMate:
                break;
            case ActorState.Mating:
                break;
            case ActorState.LookingForWater:
                break;
            case ActorState.Drinking:
                break;
            case ActorState.RunningAway:
                break;
            case ActorState.Chilling:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public GameObject FindClosestFood()
    {
        var gos = GameObject.FindGameObjectsWithTag("Food");
        GameObject closest = null;
        var distance = Mathf.Infinity;
        var position = transform.position;
        foreach (var go in gos)
        {
            var diff = go.transform.position - position;
            var curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    
    private enum ActorState
    {
        LookingForFood,
        Eating,
        LookingForMate,
        Mating,
        LookingForWater,
        Drinking,
        RunningAway,
        Chilling
    }
}

