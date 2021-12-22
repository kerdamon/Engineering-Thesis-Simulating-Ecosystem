using System.Collections.Generic;
using System.Linq;
using DecisionMaking.States;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private List<MainState> _mainStatesList; //main states are states that can be switched to regardless of current state

    [SerializeField] private GameObject mainStates;
    public IList<EventState> EventStates { get; set; }
    
    public State CurrentState { get; private set; }

    private void Start()
    {
        _mainStatesList = mainStates.GetComponents<MainState>().ToList();
        ChangeStateTo(_mainStatesList[0]);
    }

    private void Update()
    {
        var thereIsActiveEventState = InferEventState();
        if (!thereIsActiveEventState)
        {
            InferState();
        }
        
    }

    private bool InferEventState()
    {
        // if (EventStates.Count <= 0)
        //     return false;
        // CurrentState = EventStates.Last();
        // return true;
        return false;
        //todo implement this
    }

    private void InferState()
    {
        var newStateCandidate = _mainStatesList.Aggregate((state1, state2) => state1.CurrentRank > state2.CurrentRank ? state1 : state2);
        if (newStateCandidate.CurrentRank > ((MainState)CurrentState).CurrentRank)
        {
            ChangeStateTo(newStateCandidate);
        }
    }

    private void ChangeStateTo(State newState)
    {
        CurrentState = newState;
        CurrentState.prepareModel();
    }
}

