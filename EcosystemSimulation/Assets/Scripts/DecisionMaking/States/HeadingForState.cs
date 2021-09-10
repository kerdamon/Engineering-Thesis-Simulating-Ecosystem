﻿using DefaultNamespace;
using Interactions;
using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class HeadingForState : State
    {
        protected ActorActions Actions;
        protected Sensors Sensors;

        private StateMachine _stateMachine;
        protected InteractionState NextState;
        protected LookingForState PreviousState;
        
        public virtual void Start()
        {
            Actions = GetComponentInParent<ActorActions>();
            Sensors = GetComponentInParent<Sensors>();
            _stateMachine = GetComponentInParent<StateMachine>();
        }

        public override void Act()
        {
            var target = FindTarget();
            try
            {
                Actions.MoveToPointUpToDistance(target.transform.position);
            }
            catch (TargetNotFoundException)
            {
                SwitchToPreviousState();
            }
            
            if (CanSwitchToNextState())
            {
                SwitchToNextState();
            }
        }

        protected abstract GameObject FindTarget();

        protected abstract bool CanSwitchToNextState();
        
        private void SwitchToNextState()
        {
            _stateMachine.CurrentState = NextState;
        }

        private void SwitchToPreviousState()
        {
            _stateMachine.CurrentState = PreviousState; 
        }

        public override float CurrentRank => PreviousState.CurrentRank + 1;
    }
}