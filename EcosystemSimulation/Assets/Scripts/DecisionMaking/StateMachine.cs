using System.Collections.Generic;
using System.Linq;
using DecisionMaking.States;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private List<State> _mainStatesList; //main states are states that can be switched to regardless of current state

    [SerializeField] private GameObject _mainStates;
    
    public State CurrentState { get; set; }

    private void Start()
    {
        _mainStatesList = _mainStates.GetComponents<State>().ToList();
        InferState();
    }

    private void Update()
    {
        InferState();
        CurrentState.Act();
    }

    private void InferState()
    {
        Debug.Log($"_statesList.States[0].CurrentRank {_mainStatesList[0].CurrentRank} ");
        Debug.Log($"_statesList.States[1].CurrentRank {_mainStatesList[1].CurrentRank} ");
        CurrentState = _mainStatesList.Aggregate((state1, state2) => state1.CurrentRank > state2.CurrentRank ? state1 : state2);
    }
}

