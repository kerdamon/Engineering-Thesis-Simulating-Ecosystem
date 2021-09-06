using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class HeadingForFoodState : State
    {
        private Needs _needs;
        private ActorActions _actions;
        private Sensors _sensors;
        
        private StateMachine _stateMachine;
        private EatingState _nextState;
        private LookingForFoodState _previousState;
        
        public void Start()
        {
            _needs = GetComponentInParent<Needs>();
            _actions = GetComponentInParent<ActorActions>();
            _sensors = GetComponentInParent<Sensors>();
            _stateMachine = GetComponentInParent<StateMachine>();
            var parent = transform.parent;
            _nextState = parent.gameObject.GetComponentInChildren<EatingState>();
            _previousState = parent.gameObject.GetComponentInChildren<LookingForFoodState>(); 
        }

        public override void Act()
        {
            var closestFoodPosition = _sensors.ClosestFoodPositionInSensorsRange();
            Debug.Log($"Acting");
            try
            {
                _actions.MoveToPointUpToDistance(closestFoodPosition.transform.position); //todo not working
            }
            catch (TargetNotFoundException)
            {
                Debug.Log($"Target Not Found");
            }
            
            if (_actions.ActorsAreInInteractionRange(gameObject, closestFoodPosition))  //todo abstract this into checking and next state as in looking for food state
            {
                _stateMachine.CurrentState = _nextState;
            }
        }

        public override float CurrentRank => _previousState.CurrentRank + 1;
    }
}