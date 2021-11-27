using Input;
using Interactions;
using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class InteractionState : State
    {
        protected Sensors Sensors;
        protected Interactor Interactor;
        
        private StateMachine _stateMachine;
        protected HeadingForState PreviousState;
        protected ChillingState NextState;

        public virtual void Start()
        {
            Sensors = GetComponentInParent<Sensors>();
            _stateMachine = GetComponentInParent<StateMachine>();
            Interactor.AfterInteraction = () => _stateMachine.CurrentState = NextState;
        }

        public override float CurrentRank => PreviousState.CurrentRank + 1;
    }
}