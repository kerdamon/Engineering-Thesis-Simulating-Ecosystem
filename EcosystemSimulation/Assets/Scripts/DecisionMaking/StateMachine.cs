using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DecisionMaking;
using DecisionMaking.States;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(StatesList))]
public class StateMachine : MonoBehaviour
{
    private StatesList _statesList;

    public IState CurrentState { get; set; }

    private void Start()
    {
        _statesList = GetComponent<StatesList>();
        CurrentState = _statesList.States[0];
    }

    private void Update()
    {
        InferState();
        CurrentState.Act();
    }

    private void InferState()
    {
        Debug.Log($"_statesList.States[0].CurrentRank {_statesList.States[0].CurrentRank} ");
        Debug.Log($"_statesList.States[1].CurrentRank {_statesList.States[1].CurrentRank} ");
        CurrentState = _statesList.States.Aggregate((state1, state2) => state1.CurrentRank > state2.CurrentRank ? state1 : state2);
    }

    // private void ActOnState()
    // {
    //     switch (CurrentState)
    //     {
    //         case ActorState.LookingForFood:
    //         case ActorState.LookingForMate:
    //                 _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
    //             break;
    //         case ActorState.HeadingForFood:
    //             try
    //             {
    //                 _actions.MoveToPointUpToDistance(_sensors.ClosestFoodPositionInSensorsRange().transform.position);
    //             }
    //             catch (TargetNotFoundException)
    //             { }
    //             break;
    //         case ActorState.HeadingToMate:
    //             try
    //             {
    //                 _actions.MoveToPointUpToDistance(_sensors.ClosestPartnerPositionInSensoryRange().transform.position);
    //             }
    //             catch (TargetNotFoundException)
    //             { }
    //             break;
    //         case ActorState.Eating:
    //             _eatingInteractor.Interact(gameObject, _sensors.ClosestFoodPositionInSensorsRange(), 0);
    //             break;
    //         case ActorState.Mating:
    //             break;
    //         case ActorState.LookingForWater:
    //             break;
    //         case ActorState.Drinking:
    //             break;
    //         case ActorState.RunningAway:
    //             break;
    //         case ActorState.Chilling:
    //             break;
    //         case ActorState.HeadingToWater:
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }

    // public enum ActorState
    // {
    //     LookingForFood,     //does not see food, wandering looking for it
    //     HeadingForFood,        //knows where are food, and heading for it
    //     Eating,
    //     LookingForMate,
    //     HeadingToMate,
    //     Mating,
    //     LookingForWater,
    //     HeadingToWater,
    //     Drinking,
    //     RunningAway,
    //     Chilling
    // }
}

