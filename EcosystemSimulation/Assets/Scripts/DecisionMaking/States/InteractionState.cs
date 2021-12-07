using Input;
using Interactions;
using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class InteractionState : State
    {
        protected Sensors Sensors;
        protected Interaction Interaction;
        
        private StateMachine _stateMachine;
        protected HeadingForState PreviousState;
        protected ChillingState NextState;

        public virtual void Start()
        {
            Sensors = GetComponentInParent<Sensors>();
            _stateMachine = GetComponentInParent<StateMachine>();
            Interaction.AfterInteraction = () => _stateMachine.CurrentState = NextState;
        }

        public override float CurrentRank => PreviousState.CurrentRank + 1;
    }
}