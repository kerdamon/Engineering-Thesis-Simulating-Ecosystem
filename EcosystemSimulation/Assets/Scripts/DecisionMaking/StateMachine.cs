using System.Collections.Generic;
using System.Linq;
using DecisionMaking.States;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private GameObject states;
    
    public State CurrentState { get; private set; }
    private List<State> _statesList; //main states are states that can be switched to regardless of current state
    
    private void Start()
    {
        _statesList = states.GetComponents<State>().ToList();
        ChangeStateTo(_statesList[0]);
    }

    private void Update()
    {
        InferState();
    }

    private void InferState()
    {
        var newStateCandidate = _statesList.Aggregate((state1, state2) => state1.CurrentRank > state2.CurrentRank ? state1 : state2);
        if (newStateCandidate.CurrentRank > CurrentState.CurrentRank)
        {
            ChangeStateTo(newStateCandidate);
        }
    }

    private void ChangeStateTo(State newState)
    {
        CurrentState = newState;
        CurrentState.PrepareModel();
    }
}

