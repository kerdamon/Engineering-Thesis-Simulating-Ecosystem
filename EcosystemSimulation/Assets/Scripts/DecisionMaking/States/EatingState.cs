using UnityEngine;

namespace DecisionMaking.States
{
    public class EatingState : State
    {
        private Sensors _sensors;
        private EatingInteractor _eatingInteractor;
        
        private StateMachine _stateMachine;
        private HeadingForFoodState _previousState;
        private ChillingState _nextState;

        public void Start()
        {
            _sensors = GetComponentInParent<Sensors>();
            _eatingInteractor = GetComponentInParent<EatingInteractor>();
            _stateMachine = GetComponentInParent<StateMachine>();
            var parent = transform.parent;
            _previousState = parent.gameObject.GetComponentInChildren<HeadingForFoodState>();
            _nextState = parent.gameObject.GetComponentInChildren<ChillingState>(); 
            _eatingInteractor.AfterInteraction = () => _stateMachine.CurrentState = _nextState;
        }

        public override void Act()
        {
            _eatingInteractor.Interact(gameObject, _sensors.ClosestFoodPositionInSensorsRange(), 0);
        }

        public override float CurrentRank => _previousState.CurrentRank + 1;
    }
}