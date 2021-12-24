using System.Collections.Generic;
using System.Linq;
using DecisionMaking.States;
using Interactions;
using UnityEngine;

namespace DecisionMaking
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private GameObject states;
        [SerializeField] private State defaultState;
    
        public State CurrentState { get; private set; }
        private List<State> _statesList; //main states are states that can be switched to regardless of current state
        private InteractionManager _interactionManager;
        
        private void Start()
        {
            _interactionManager = transform.parent.GetComponentInChildren<InteractionManager>();
            _statesList = states.GetComponents<State>().ToList();
            ChangeStateTo(defaultState);
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
            _interactionManager.StopInteraction();
            CurrentState = newState;
            CurrentState.PrepareModel();
        }
    }
}

