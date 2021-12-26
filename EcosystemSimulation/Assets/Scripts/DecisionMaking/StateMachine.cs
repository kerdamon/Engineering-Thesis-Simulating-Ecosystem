using System.Collections.Generic;
using System.Linq;
using DecisionMaking.States;
using UnityEngine;

namespace DecisionMaking
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private GameObject states;
        [SerializeField] private State defaultState;
    
        public State CurrentState { get; private set; }
        private List<State> _statesList; //main states are states that can be switched to regardless of current state

        
        private void Start()
        {
            _statesList = states.GetComponents<State>().ToList();
            SetState(defaultState);
            
            GetComponentInParent<MovementAgent>().AfterAction += InferState;
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
            CurrentState.OnLeaveState();
            SetState(newState);
        }

        private void SetState(State newState)
        {
            CurrentState = newState;
            CurrentState.PrepareModel(); 
        }
    }
}

