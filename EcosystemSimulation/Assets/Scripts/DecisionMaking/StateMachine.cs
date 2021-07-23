using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Needs))]
[RequireComponent(typeof(Sensors))]
public class StateMachine : MonoBehaviour
{
    private ActorActions _actions;
    
    private Needs _needs;
    private Sensors _sensors;

    private ActorState _currentState;
    
    private void Start()
    {
        _actions = GetComponent<ActorActions>();
        _needs = GetComponent<Needs>();
        _sensors = GetComponent<Sensors>();
    }

    private void Update()
    {
        InferState();
        ActOnState();

    }

    private void InferState()
    {
        _currentState = ActorState.Chilling;
        if (!(_needs.NeedsDictionary["Hunger"] > 80)) return;   //if is not hungry enough it just chills
        
        //else tries to find food
        try
        {
            _sensors.ClosestFoodPositionInSensorsRange();
            _currentState = ActorState.HeadForFood;
        }
        catch (TargetNotFoundException)
        {
            _currentState = ActorState.LookingForFood;
        }
    }

    private void ActOnState()
    {
        switch (_currentState)
        {
            case ActorState.LookingForFood:
                    _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
                break;
            case ActorState.HeadForFood:
                try
                {
                    _actions.MoveToPoint(_sensors.ClosestFoodPositionInSensorsRange());
                }
                catch (TargetNotFoundException)
                { }
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
    
    private enum ActorState
    {
        LookingForFood,     //does not see food, wandering looking for it
        HeadForFood,        //knows where are food, and heading for it
        Eating,
        LookingForMate,
        Mating,
        LookingForWater,
        Drinking,
        RunningAway,
        Chilling
    }
}

