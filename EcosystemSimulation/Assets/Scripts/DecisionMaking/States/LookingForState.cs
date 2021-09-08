using System.Threading;
using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class LookingForState : State
    {
        protected Needs Needs;
        private ActorActions _actions;
        protected Sensors Sensors;

        private StateMachine _stateMachine;
        protected HeadingForState NextState;
        
        public virtual void Start()
        {
            Needs = GetComponentInParent<Needs>();
            _actions = GetComponentInParent<ActorActions>();
            Sensors = GetComponentInParent<Sensors>();
            _stateMachine = GetComponentInParent<StateMachine>();
        }

        public override void Act()
        {
            _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
            if (CanSwitchToNextState())
            {
                SwitchToNextState();
            }
        }

        protected abstract bool CanSwitchToNextState();
        
        private void SwitchToNextState()
        {
            _stateMachine.CurrentState = NextState;
        }
    }
}