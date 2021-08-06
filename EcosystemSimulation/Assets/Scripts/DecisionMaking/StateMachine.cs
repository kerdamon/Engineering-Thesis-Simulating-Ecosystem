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
    private MatingInteractor _matingInteractor;
    private EatingInteractor _eatingInteractor;

    private bool _matingWasInvoked = false;     //todo remove, only temporary to see if it works

    public ActorState CurrentState { get; private set; }

    private void Start()
    {
        _actions = GetComponent<ActorActions>();
        _needs = GetComponent<Needs>();
        _sensors = GetComponent<Sensors>();
        _matingInteractor = GetComponent<MatingInteractor>();
        _eatingInteractor = GetComponent<EatingInteractor>();
    }

    private void Update()
    {
        InferState();
        ActOnState();

    }

    private void InferState()
    {
        if (_needs["Hunger"] > 80)
        {
            try
            {
                var closestFoodPosition = _sensors.ClosestFoodPositionInSensorsRange();
                CurrentState = ActorState.HeadingForFood;
                if (_actions.ActorsAreInInteractionRange(gameObject, closestFoodPosition))
                {
                    CurrentState = ActorState.Eating;
                }
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
                _eatingInteractor.Interact(gameObject, _sensors.ClosestFoodPositionInSensorsRange(), 0);
                break;
            case ActorState.Mating:
                if (!_matingWasInvoked)
                {
                    _matingInteractor.Interact(gameObject, _sensors.ClosestSameSpeciesActorPositionInSensoryRange(), 5);
                    _matingWasInvoked= true;
                }
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

