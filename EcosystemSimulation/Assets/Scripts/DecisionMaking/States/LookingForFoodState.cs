using System.Threading;
using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForFoodState : State
    {
        private Needs _needs;
        private ActorActions _actions;
        private Sensors _sensors;

        private StateMachine _stateMachine;
        private HeadingForFoodState _nextState;
        
        public void Start()
        {
            _needs = GetComponentInParent<Needs>();
            _actions = GetComponentInParent<ActorActions>();
            _sensors = GetComponentInParent<Sensors>();
            _stateMachine = GetComponentInParent<StateMachine>();
            _nextState = transform.parent.gameObject.GetComponentInChildren<HeadingForFoodState>();
        }

        public override void Act()
        {
            _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
            if (CanSwitchToNextState())
            {
                SwitchToNextState();
            }
        }

        private bool CanSwitchToNextState()
        {
            try
            {
                _sensors.ClosestFoodPositionInSensorsRange();
                return true;
            }
            catch (ThreadStartException)
            {
                return false;
            }
        }
        
        private void SwitchToNextState()
        {
            _stateMachine.CurrentState = _nextState;
        }
        
        public override float CurrentRank => _needs["Hunger"];
    }
}