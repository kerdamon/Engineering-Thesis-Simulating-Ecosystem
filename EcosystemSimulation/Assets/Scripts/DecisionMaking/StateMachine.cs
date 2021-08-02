using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Needs))]
[RequireComponent(typeof(Sensors))]
public class StateMachine : MonoBehaviour
{
    private ActorActions _actions;
    
    private Needs _needs;
    private Sensors _sensors;
    private MatingController _matingController;

    private int _matingCounter;

    [SerializeField] private int matingCounterMax;

    public ActorState CurrentState { get; private set; }

    private void Start()
    {
        _actions = GetComponent<ActorActions>();
        _needs = GetComponent<Needs>();
        _sensors = GetComponent<Sensors>();
        _matingController = GetComponent<MatingController>();
    }

    private void Update()
    {
        InferState();
        ActOnState();

    }

    private void InferState()
    {
        if (_needs.NeedsDictionary["Hunger"] > 80)
        {
            try
            {
                _sensors.ClosestFoodPositionInSensorsRange();
                CurrentState = ActorState.HeadingForFood;
            }
            catch (TargetNotFoundException)
            {
                CurrentState = ActorState.LookingForFood;
            } 
        }
        else
        {
            try
            {
                var closestSameSpeciesActor = _sensors.ClosestSameSpeciesActorPositionInSensoryRange();
                CurrentState = ActorState.HeadingToMate;
                if (_actions.ActorsAreInInteractionRange(gameObject, closestSameSpeciesActor))
                {
                    CurrentState = ActorState.Mating;
                }
            }
            catch (TargetNotFoundException)
            {
                CurrentState = ActorState.LookingForMate;
            }  
        }
        

    }

    private void ActOnState()
    {
        switch (CurrentState)
        {
            case ActorState.LookingForFood:
            case ActorState.LookingForMate:
                    _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
                break;
            case ActorState.HeadingForFood:
                try
                {
                    _actions.MoveToPointUpToDistance(_sensors.ClosestFoodPositionInSensorsRange().transform.position);
                }
                catch (TargetNotFoundException)
                { }
                break;
            case ActorState.HeadingToMate:
                try
                {
                    _actions.MoveToPointUpToDistance(_sensors.ClosestSameSpeciesActorPositionInSensoryRange().transform.position);
                }
                catch (TargetNotFoundException)
                { }
                break;
            case ActorState.Eating:
                break;
            case ActorState.Mating:
                if(++_matingCounter > matingCounterMax)
                    _matingController.SpawnOffspring(gameObject, _sensors.ClosestSameSpeciesActorPositionInSensoryRange());
                break;
            case ActorState.LookingForWater:
                break;
            case ActorState.Drinking:
                break;
            case ActorState.RunningAway:
                break;
            case ActorState.Chilling:
                break;
            case ActorState.HeadingToWater:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public enum ActorState
    {
        LookingForFood,     //does not see food, wandering looking for it
        HeadingForFood,        //knows where are food, and heading for it
        Eating,
        LookingForMate,
        HeadingToMate,
        Mating,
        LookingForWater,
        HeadingToWater,
        Drinking,
        RunningAway,
        Chilling
    }
}

