using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class HeadingForFoodState : State
    {
        public override float Priority => 0;
        private Needs _needs;
        private ActorActions _actions;
        private Sensors _sensors;
        
        private StateMachine _stateMachine;
        private EatingState _nextState;
        
        public void Start()
        {
            _needs = GetComponentInParent<Needs>();
            _actions = GetComponentInParent<ActorActions>();
            _sensors = GetComponentInParent<Sensors>();
            _stateMachine = GetComponentInParent<StateMachine>();
            _nextState = transform.parent.gameObject.GetComponentInChildren<EatingState>();
        }

        public override void Act()
        {
            var closestFoodPosition = _sensors.ClosestFoodPositionInSensorsRange();

            try
            {
                _actions.MoveToPointUpToDistance(closestFoodPosition.transform.position);   //todo not working
            }
            catch (TargetNotFoundException)
            { }
            
            if (_actions.ActorsAreInInteractionRange(gameObject, closestFoodPosition))  //todo abstract this into checking and next state as in looking for food state
            {
                _stateMachine.CurrentState = _nextState;
            }
        }

        public override float CurrentRank => Priority;
    }
}