using System.Collections.Generic;
using System.Linq;
using DecisionMaking.States;
using Unity.MLAgents;
using UnityEngine;

namespace DecisionMaking
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State defaultState;
    
        public State CurrentState { get; private set; }
        private List<State> _statesList; //main states are states that can be switched to regardless of current state

        private bool is_training;
        
        private void Start()
        {
            _statesList = GetComponentsInChildren<State>().ToList();
            SetDefaultState();
            CurrentState.OnEnterState();
            is_training = Academy.Instance.EnvironmentParameters.GetWithDefault("is_training", 0.0f) > 0.00001f;
        }

        protected virtual void SetDefaultState()
        {
           SetState(defaultState);
        }
        
        protected virtual void Update(){
            if (!is_training)
            {
                InferState();
            }
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
            CurrentState.OnEnterState();
        }

        private void SetState(State newState)
        {
            CurrentState = newState;
        }
    }
}

