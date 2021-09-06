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
        CurrentState = _mainStatesList[0];
    }

    private void Update()
    {
        InferState();
        CurrentState.Act();
    }

    private void InferState()
    {
        var newStateCandidate = _mainStatesList.Aggregate((state1, state2) => state1.CurrentRank > state2.CurrentRank ? state1 : state2);
        if (newStateCandidate.CurrentRank > CurrentState.CurrentRank)
        {
            CurrentState = newStateCandidate;
        }
    }
}

